using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_XDrawer
{
    [Serializable]
    public class Circle : TwoPointFigure
    {
        public Circle(Popup popup, int x, int y) // constructor
            : base(popup, x, y)  // 상위 클래스 constructor 호출
        {
        }
        public Circle(Popup popup, int x1, int y1, int x2, int y2) // clone 함수를 위해 만든 constructor
            : base(popup, x1, y1, x2, y2)
        {
            _fillFlag = false;
        }


        private bool _fillFlag;

        // 아래 HDC와 Circle을 사용하기 위해 C의 DLL을 가져온다
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern int Ellipse(IntPtr hdc, int ulCornerX, int ulCornerY, int lrCornerX, int lrCornerY); // C언어에서 원을 그리는 Ellipse 함수 선언

        public override void draw(IntPtr hdc) // 기존 Graphics 객체 대신 HDC를 사용하는 draw 함수
        {
            Ellipse(hdc, _x1, _y1, _x2, _y2); // C언어에서 사용하는 원을 그리는 함수
        }

        public override void draw(Graphics g, Pen pen)
        {
            Color oldColor = pen.Color; // 기존의 펜 색을 저장
            pen.Color = _color; // 펜의 색을 바꿔줌
            // 시작 위치가 끝 위치보다 큰 경우 제대로 그려지지 않고 width와 height가 -값이 되면 사각형이 그려지지 않는다
            g.DrawEllipse(pen, Math.Min(this._x1, this._x2), Math.Min(this._y1, this._y2), Math.Abs(this._x2 - this._x1), Math.Abs(this._y2 - this._y1));
            // this 안써도 됨
            pen.Color = oldColor; // 기존의 펜 색으로 돌려놓음
            
            // 팝업의 채우기에 대해 처리해주는 곳
            if(_fillFlag == true)
            {
                Brush br = new SolidBrush(_color); // 색을 가진 brush 할당
                g.FillEllipse(br, Math.Min(this._x1, this._x2), Math.Min(this._y1, this._y2), Math.Abs(this._x2 - this._x1), Math.Abs(this._y2 - this._y1)); // 위의 draw와 좌표는 동일

                br.Dispose(); // brush 클래스는 어지간하면 직접 garbage collection 해주는 게 권고 사항
            }
        }

        // 채우기를 사용할지 안할지 정하는 함수
        public override void setFill()
        {
            _fillFlag = !_fillFlag; // FALSE면 TRUE로 TRUE면 FALSE로
        }
        // 원 그림을 복사하는 이벤트 핸들러
        public override Figure clone()
        {
            Circle newFigure = new Circle(_popup, _x1, _y1, _x2, _y2); // 현재 circle값을 가진 새 circle 클래스를 만듬
            newFigure._color = _color; // 색 정하기
            newFigure._fillFlag = _fillFlag; // 채우기 정하기

            return newFigure;
        }
        // 상속받아 클래스 이름을 넘겨주는 함수
        public override String getClassName()
        {
            return "Circle";
        }
    }
}
