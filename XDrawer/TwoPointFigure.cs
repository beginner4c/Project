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
    public abstract class TwoPointFigure : Figure
    {
        // constructor
        public TwoPointFigure(Popup popup, int x, int y)
            : base(popup)
        {
            _x1 = _x2 = x;
            _y1 = _y2 = y;
        }
        public TwoPointFigure(Popup popup, int x1, int y1, int x2, int y2)
            : base(popup)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        // data member
        protected int _x1;
        protected int _y1;
        protected int _x2;
        protected int _y2;
        
        // Figure class에서 내려받아 override 하는 함수
        public override int getX1() // X,Y 좌표를 돌려주는 함수들
        {
            return _x1;
        }
        public override int getY1()
        {
            return _y1;
        }
        public override int getX2()
        {
            return _x2;
        }
        public override int getY2()
        {
            return _y2;
        }

        // set 함수
        public override void setXY2(int x, int y) // 상위 클래스인 Figure에서 재정의한 함수
        {
            _x2 = x;
            _y2 = y;
        }
        // Kite의 좌표를 세팅하기 위한 함수
        public override void setXY12(int x1, int y1, int x2, int y2)
        {
            _x1 = x1; _y1 = y1;
            _x2 = x2; _y2 = y2;
        }

        public override void makeRegion() // 완성된 그림의 범위를 잡아서 기억하는 함수
        {
            System.Drawing.Point[] pt = new System.Drawing.Point[4]; // 만들어진 객체들의 좌표를 저장할 객체
            pt[0].X = _x1;
            pt[0].Y = _y1;

            pt[1].X = _x2;
            pt[1].Y = _y1;

            pt[2].X = _x2;
            pt[2].Y = _y2;

            pt[3].X = _x1;
            pt[3].Y = _y2;

            byte[] type = new byte[4]; // 모서리들을 뭘로 연결할건지 나타내는 객체

            type[0] = (byte)PathPointType.Line; // 선으로 모서리들을 연결
            type[1] = (byte)PathPointType.Line;
            type[2] = (byte)PathPointType.Line;
            type[3] = (byte)PathPointType.Line;

            GraphicsPath gp = new GraphicsPath(pt, type);

            _region = new Region(gp); // 만든 그림을 인식해 줄 Region 객체를 만든다
        }

        public override void move(int dx, int dy) // 좌표를 이동시키는 함수
        {
            _x1 += dx;
            _y1 += dy;
            _x2 += dx;
            _y2 += dy;
        }
    }
}
