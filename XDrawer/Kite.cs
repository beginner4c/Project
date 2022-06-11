using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Aggregation을 통해서 연 모양을 그릴 거다
// 원과 선 4개, 사각형을 좌표에 맞추어 그린다
// 현재 복사하기를 이용하면 Kite 그림 내부의 원이 늘어진다
// 팝업의 채우기 기능은 구현x

namespace _24_XDrawer
{
    [Serializable]
    public class Kite : Box
    {
        
        public Kite(Popup popup, int x, int y) // constructor
            : base(popup, x, y)
        {
            circle = new Circle(popup, x, y);
            line1 = new Line(popup, x, y);
            line2 = new Line(popup, x, y);
            line3 = new Line(popup, x, y);
            line4 = new Line(popup, x, y);
        }
        public Kite(Popup popup, int x1, int y1, int x2, int y2)
            : base(popup, x1, y1, x2, y2)
        {
            int w = Math.Abs(x2 - x1);
            int h = Math.Abs(y2 - y1);
            int x = Math.Min(x1, x2);
            int y = Math.Min(y1, y2);
            circle = new Circle(popup, x + w / 4, y + h / 4, x + 3 * w / 4, y + 3 * h / 4);
            line1 = new Line(popup, x1, y1, x2, y2);
            line2 = new Line(popup, x1 + w / 2, y1, x1 + w / 2, y2);
            line3 = new Line(popup, x2, y1, x1, y2);
            line4 = new Line(popup, x1, y1 + h / 2, x2, y1 + h / 2);
        }
        //data member
        Circle circle;
        Line line1;
        Line line2;
        Line line3;
        Line line4;

        // Kite를 그리기 위한 함수, dialog에서 사용
        public override void draw(Graphics g, Pen pen)
        {
            base.draw(g, pen);
            // delegation 위임
            circle.draw(g, pen);
            line1.draw(g, pen);
            line2.draw(g, pen);
            line3.draw(g, pen);
            line4.draw(g, pen);
        }
        // 그려진 Kite를 옮기기 위한 함수
        public override void move(int dx, int dy)
        {
            base.move(dx, dy);
            // delegation
            circle.move(dx, dy);
            line1.move(dx, dy);
            line2.move(dx, dy);
            line3.move(dx, dy);
            line4.move(dx, dy);
        }
        // rubber banding으로 그림을 그리기 위한 함수
        public override void draw(IntPtr hdc)
        {
            base.draw(hdc);
            // delegation
            circle.draw(hdc);
            line1.draw(hdc);
            line2.draw(hdc);
            line3.draw(hdc);
            line4.draw(hdc);
        }
        // Kite 그림의 색을 바꿀 함수
        public override void setColor(Color color)
        {
            base.setColor(color);
            circle.setColor(color);
            line1.setColor(color);
            line2.setColor(color);
            line3.setColor(color);
            line4.setColor(color);
        }

        // 팝업에서 복사하기 눌렀을 때 복사하기
        public override Figure clone()
        {
            Kite newFigure = new Kite(_popup, _x1, _y1, _x2, _y2); // 현재 박스값을 가진 새 박스 클래스를 만듬
            newFigure.setColor(_color); // 색 정하기

            return newFigure;
        }
        // 그릴 좌표를 세팅하는 함수, 이 함수 이전에는 그려도 사각형만 그려졌다
        public override void setXY2(int newX, int newY)
        {
            base.setXY2(newX, newY);
            int w = newX - _x1;
            int h = newY - _y1;
            circle.setXY12(_x1 + w / 4, _y1 + h / 4, _x1 + 3 * w / 4, _y1 + 3 * h / 4);
            line1.setXY12(_x1, _y1, newX, newY);
            line2.setXY12(_x1 + w / 2, _y1, _x1 + w / 2, newY);
            line3.setXY12(newX, _y1, _x1, newY);
            line4.setXY12(_x1, _y1 + h / 2, newX, _y1 + h / 2);
        }
    }
}
