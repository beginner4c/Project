using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_XDrawer
{
    [Serializable] // Save, Load를 위해서 Serializable만 명시해주면 가능해진다
    public abstract class Figure // 메인 폼에서 함수의 parameter로 활용하기 위해 public 선언
    {
        protected Color _color; // 그림의 색을 정할 거

        // Nonserialized 를 명시해 save, load하지 않음을 명시할 수 있다
        // Window 객체는 serialize가 불가능하다
        [NonSerialized] // data member마다 따로 설정해주어야 한다
        protected Region _region; // 이걸 상속받는 TwoPointFigure가 사용할 수 있게 visibility 설정        
        [NonSerialized]
        protected Popup _popup; // 각 그림마다 자기 팝업을 띄우기 위한 reference를 유지할 수 있게

        public Figure(Popup popup)
        {
            _popup = popup;
            _color = Color.Black; // defalut 값은 검정색으로 주게 초기화
        }

        // load 시에 각 그림에 자기 팝업을 다시 세팅해주기 위한 함수
        public void setPopup(Popup popup)
        {
            _popup = popup;
        }

        // 색을 지정할 함수, Kite 때문에 virtual로 설정
        public virtual void setColor(Color color)
        {
            _color = color;
        }
        // 상속할 함수들
        // 상속받은 클래스에서 어떻게든 재정의해야하는 함수 바디가 없는 abstract (하위 클래스에서 꼭 override 해줘야 한다)
        public abstract void draw(Graphics g, Pen pen); // 그림 그릴 함수

        public abstract void draw(IntPtr hdc); // HDC를 이용해 그림을 그릴 함수

        public abstract void setXY2(int x, int y); // 좌표 설정 함수

        public virtual void setXY12(int x1, int y1, int x2, int y2)
        {
            // hook function
        }

        public abstract void makeRegion(); // region 만드는 함수

        public abstract void move(int dx, int dy); // 좌표 이동 시키는 함수

        // 상속받는 특정 클래스에서만 사용하는 함수는 virtual로 함수 바디를 비워놓고 상속시킨다(꼭 override 할 필요 없다)
        public virtual void setFill() // Box와 Circle class에서만 사용할 함수이다
        {

        }
        
        public virtual void drawing(IntPtr hdc, int newX, int newY) // draw와 setXY2 함수를 한 번에 사용할 함수
        {
            // dynamic biding을 통해서 this가 box라면 box의 draw를 호출하게 된다
            this.draw(hdc); // 기존에 그려진 사각형을 지울 사각형을 그린다
            this.setXY2(newX, newY); // x2, y2, 값을 새로 세팅
            this.draw(hdc); // 마우스가 움직이면서 받은 새 좌표로 사각형을 그린다
        }

        public virtual void move(IntPtr hdc, int dx, int dy) // 그림을 옮길 때 좌표 계산에 사용할 함수
        {
            draw(hdc); // SetROP2 효과에 의해서 같은 위치의 선은 지워지게 된다
            move(dx, dy); // 좌표를 이동한다
            draw(hdc); // 변경된 좌표로 도형을 다시 그린다
        }

        public bool ptInRegion(int x, int y) // 마우스 위치에 만들어진 region이 있는지 판별하는 함수
        {
            if (_region != null) // 마우스 위치에 만들어진 그림이 있으면
            {
                return _region.IsVisible(x, y); // 해당 좌표에 있다고 반환
            }
            else // 마우스 위치에 그림이 없으면
            {
                return false; // 없다고 반환
            }
        }

        public void popup(System.Drawing.Point pos) // 좌표를 받아서 상속받은 클래스들이 맞게 팝업을 할 수 있게
        {
            // Delegation 위임
            _popup.popup(pos);
        }

        public abstract Figure clone();

        // TreeForm에서 사용할 Abstract function / Hook Function
        public virtual int getX1() // 실제 의미있는 함수는 child class에서 override를 통해서 구현
        {
            return -1;
        }
        public virtual int getY1()
        {
            return -1;
        }
        public virtual int getX2()
        {
            return -1;
        }
        public virtual int getY2()
        {
            return -1;
        }
        // ListForm 혹은 TreeForm에서 사용할 클래스 이름을 넘겨줄 함수
        public virtual String getClassName()
        {
            return "Figure";
        }

    }
}
