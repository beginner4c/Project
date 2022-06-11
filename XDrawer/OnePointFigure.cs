using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 이 클래스도 abstract 클래스로 만들어줘야 한다
// 이유는 abstract 클래스를 상속받은 이 곳에서 draw 함수를 override를 하지 않고 또 상속을 내리기 때문
// instantiation 할 수 없는 클래스이다

namespace _24_XDrawer
{
    [Serializable]
    public abstract class OnePointFigure : Figure // 사실상 점을 만들어 주기 위한 클래스
    {
        // constructor
        public OnePointFigure(Popup popup, int x, int y)
            : base(popup)
        {
            _x1 = x;
            _y1 = y;
        }

        // data member
        protected int _x1;
        protected int _y1;
        protected static int Delta = 5;

        // Figure class에서 내려받아 override 하는 함수
        public override int getX1() // X,Y 좌표를 돌려주는 함수들
        {
            return _x1;
        }
        public override int getY1()
        {
            return _y1;
        }

        // set 함수
        public override void setXY2(int x, int y) // 상위 클래스인 Figure에서 재정의한 함수
        {
            _x1 = x;
            _y1 = y;
        }

        public override void makeRegion() // 완성된 그림의 범위를 잡아서 기억하는 함수
        {
            System.Drawing.Point[] pt = new System.Drawing.Point[4]; // 만들어진 객체들의 좌표를 저장할 객체
            
            // 점의 region을 잡기 위해 좌표 확정
            pt[0].X = _x1-Delta;
            pt[0].Y = _y1-Delta;

            pt[1].X = _x1+Delta;
            pt[1].Y = _y1-Delta;

            pt[2].X = _x1+Delta;
            pt[2].Y = _y1+Delta;

            pt[3].X = _x1-Delta;
            pt[3].Y = _y1+Delta;

            byte[] type = new byte[4]; // 모서리들을 뭘로 연결할건지 나타내는 객체

            type[0] = (byte)PathPointType.Line; // 선으로 모서리들을 연결
            type[1] = (byte)PathPointType.Line;
            type[2] = (byte)PathPointType.Line;
            type[3] = (byte)PathPointType.Line;

            GraphicsPath gp = new GraphicsPath(pt, type);

            _region = new Region(gp); // 만든 그림을 인식해 줄 Region 객체를 만든다
        }
        public override void move(int dx, int dy) // 좌표를 이동시킬 함수
        {
            _x1 += dx;
            _y1 += dy;
        }
    }
}
