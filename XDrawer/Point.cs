using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 점을 그릴 클래스
// Point class의 경우 System.Drawing 에 라이브러리로 정의가 되어 있어서 다른 Point를 사용하는 클래스들에게 영향을 줄 수 있다
// 이런 문제점을 해결하기 위해서 다른 클래스들에서는 System.Drawing.Point라고 명확하게 정의해서 사용할 필요가 있어진다

namespace _24_XDrawer
{
    [Serializable]
    public class Point : OnePointFigure
    {
        public Point(Popup popup, int x, int y) // constructor
           : base(popup, x, y) // 상위 클래스 constructor 호출
        {

        }

        public override void draw(Graphics g, Pen pen) // 상위 클래스인 Figure에서 재정의한 함수
        {
            Color oldColor = pen.Color; // 기존의 펜 색을 저장
            pen.Color = _color; // 펜의 색을 바꿔줌
            // 시작 위치가 끝 위치보다 큰 경우 제대로 그려지지 않고 width와 height가 -값이 되면 사각형이 그려지지 않는다
            g.DrawRectangle(pen, _x1 - Delta, _y1 - Delta, 2*Delta, 2*Delta);
            // this 안써도 됨
            pen.Color = oldColor; // 기존의 펜 색으로 돌려놓음
        }

        // 아래 HDC와 Rectangle을 사용하기 위해 C의 DLL을 가져온다
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern int Rectangle(IntPtr hdc, int ulCornerX, int ulCornerY, int lrCornerX, int lrCornerY); // C언어에서 사각형을 그리는 Rectangle 함수 선언

        public override void draw(IntPtr hdc) // 기존 Graphics 객체 대신 HDC를 사용하는 draw 함수
        {
            Rectangle(hdc, _x1 - Delta, _y1 - Delta, _x1 + Delta, _y1 + Delta); // C언어에서 사용하는 사각형을 그리는 함수
        }

        // 점 그림을 복사하는 이벤트 핸들러
        public override Figure clone()
        {
            Point newFigure = new Point(_popup, _x1, _y1); // 현재 Point값을 가진 새 Point 클래스를 만듬
            newFigure._color = _color; // 색 정하기

            return newFigure;
        }
        // 클래스 이름을 넘겨주는 함수
        public override String getClassName()
        {
            return "Point";
        }
    }
}
