using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_XDrawer
{
    [Serializable]
    public class Line : TwoPointFigure
    {
        public Line(Popup popup,int x, int y) // constructor
            : base(popup, x, y) // 상위 클래스 constructor 호출
        {
        }
        public Line(Popup popup, int x1, int y1, int x2, int y2) // clone 함수를 위해 만든 constructor
            : base(popup, x1, y1, x2, y2)
        {
        }


        // 선을 그리는 함수
        public override void draw(Graphics g, Pen pen)
        {
            Color oldColor = pen.Color; // 기존의 펜 색을 저장
            pen.Color = _color; // 펜의 색을 바꿔줌
            g.DrawLine(pen, _x1, _y1, _x2, _y2);
            pen.Color = oldColor;// 기존의 펜 색으로 돌려놓음
        }

        // 선의 모양 인식하는 건 어려워서 그냥 긁어왔다
        // TwoPointFigure의 방식으로 처리하면 선을 대각선으로 하는 사각형만큼의 범위가 그려져 문제가 발생한다

        // 아래 HDC와 MoveTo와 LineTo를 사용하기 위해 C의 DLL을 가져온다
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern int MoveToEx(IntPtr hdc, int ulCornerX, int ulCornerY, int pos); // C언어에서 좌표를 이동하는 MoveToEx 함수 선언
        [System.Runtime.InteropServices.DllImport("gdi32.dll")] // 매번 가져올 때 마다 가져와줘야 한다
        internal static extern int LineTo(IntPtr hdc, int ulCornerX, int ulCornerY); // C언어에서 선을 그리는 LineTo 함수 선언

        public override void draw(IntPtr hdc) // 기존 Graphics 객체 대신 HDC를 사용하는 draw 함수
        {
            MoveToEx(hdc, _x1, _y1, 0); // C언어에서 선을 그릴 첫 좌표로 이동하는 함수
            LineTo(hdc, _x2, _y2); // 위치한 좌표에서부터 지정된 좌표까지 선을 그리는 함수
        }

        public override void makeRegion()
        {
            int regionWidth = 6; // 이걸 조절하면 잡히는 선 모양의 너비를 조절할 수 있다
            int x = _x1;
            int y = _y1;
            int w = _x2 - _x1;
            int h = _y2 - _y1;
            int sign_h = 1;
            if (h < 0) sign_h = -1;
            double angle;
            double theta = (w != 0) ? Math.Atan((double)(h) / (double)(w)) : sign_h * Math.PI / 2.0;
            if (theta < 0) theta = theta + 2 * Math.PI;
            angle = (theta + Math.PI / 2.0);
            int dx = (int)(regionWidth * Math.Cos(angle));
            int dy = (int)(regionWidth * Math.Sin(angle));
            System.Drawing.Point[] pt = new System.Drawing.Point[4];
            pt[0].X = x + dx; pt[0].Y = y + dy;
            pt[1].X = x - dx; pt[1].Y = y - dy;
            pt[2].X = x + w - dx; pt[2].Y = y + h - dy;
            pt[3].X = x + w + dx; pt[3].Y = y + h + dy;
            byte[] type = new byte[4];
            type[0] = (byte)PathPointType.Line;
            type[1] = (byte)PathPointType.Line;
            type[2] = (byte)PathPointType.Line;
            type[3] = (byte)PathPointType.Line;
            GraphicsPath gp = new GraphicsPath(pt, type);
            _region = new Region(gp);
        }
        // 선 그림을 복사하는 이벤트 핸들러
        public override Figure clone()
        {
            Line newFigure = new Line(_popup, _x1, _y1, _x2, _y2); // 현재 line값을 가진 새 line 클래스를 만듬
            newFigure._color = _color; // 색 정하기

            return newFigure;
        }
        // 클래스 이름을 넘겨주는 함수
        public override String getClassName()
        {
            return "Line";
        }
    }
}
