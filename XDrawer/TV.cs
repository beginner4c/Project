using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_XDrawer
{
    [Serializable]
    public class TV : TwoPointFigure
    {
        public TV(Popup popup, int x, int y) // constructor
        : base(popup, x, y, x + TOTAL_WIDTH, y + TOTAL_HEIGHT)
        {
            int x1, y1, x2, y2;
            bool antennaOption = true; // 안테나를 그릴지 말지 결정

            x1 = x;
            y1 = y + ANTENNA_HEIGHT;
            x2 = x + FRAME_WIDTH;
            y2 = y + TOTAL_HEIGHT;
            _frame = new Box(popup, x1, y1, x2, y2);
            x1 = x1 + FRAME_GAP;
            y1 = y1 + FRAME_GAP;
            x2 = x1 + SCREEN_WIDTH;
            y2 = y1 + SCREEN_HEIGHT;
            _screen = new Box(popup, x1, y1, x2, y2);
            x1 = x2 + FRAME_GAP;
            y1 = y1 + FRAME_GAP / 2;
            x2 = x1 + SWITCH_SIZE;
            y2 = y1 + SWITCH_SIZE;
            _channelButton = new Circle(popup, x1, y1, x2, y2);
            y1 = y1 + SWITCH_GAP;
            y2 = y1 + SWITCH_SIZE;
            _volumnButton = new Circle(popup, x1, y1, x2, y2);
            y1 = y1 + SWITCH_GAP;
            y2 = y1 + SWITCH_SIZE;
            _menuButton = new Circle(popup, x1, y1, x2, y2);
            x1 = x1 - FRAME_GAP / 3 + 2;
            y1 = y1 + SWITCH_GAP + 2;
            x2 = x1 + POWER_SWITCH_WIDTH;
            y2 = y1 + POWER_SWITCH_HEIGHT;
            _powerButton = new Box(popup, x1, y1, x2, y2);
            _antennaFlag = antennaOption;

            // 안테나 옵션에 따라 안테나를 그리고 안 그리고를 결정
            if (antennaOption == true) // 안테나가 있는 경우
            {
                int cx = x + TOTAL_WIDTH / 2;
                x1 = cx - ANTENNA_WIDTH;
                y1 = y;
                x2 = cx;
                y2 = y + ANTENNA_HEIGHT;
                _antenna1 = new Line(popup, x1, y1, x2, y2);
                x1 = cx + ANTENNA_WIDTH;
                _antenna2 = new Line(popup, x1, y1, x2, y2);
            }
            else // 안테나가 없는 경우
            {
                _antenna1 = null;
                _antenna2 = null;
            }

            // 리스트에 만든 객체들 다 때려박음
            list = new List<Figure>();
            list.Add(_frame);
            list.Add(_screen);
            list.Add(_channelButton);
            list.Add(_volumnButton);
            list.Add(_menuButton);
            list.Add(_powerButton);
        }

        // Attributes, Data Member
        private bool _antennaFlag;
        // Role Name
        private Box _frame;
        private Box _screen;
        private Circle _channelButton;
        private Circle _volumnButton;
        private Circle _menuButton;
        private Box _powerButton;
        private Line _antenna1;
        private Line _antenna2;
        private static int FRAME_WIDTH = 150;
        private static int FRAME_HEIGHT = 90;
        private static int ANTENNA_WIDTH = 30;
        private static int ANTENNA_HEIGHT = 40;
        private static int FRAME_GAP = 12;
        private static int SCREEN_WIDTH = 105;
        private static int SCREEN_HEIGHT = FRAME_HEIGHT - 2 * FRAME_GAP;
        private static int SWITCH_GAP = 17;
        private static int SWITCH_SIZE = 10;
        private static int POWER_SWITCH_WIDTH = 15;
        private static int POWER_SWITCH_HEIGHT = 8;
        private static int TOTAL_WIDTH = FRAME_WIDTH;
        private static int TOTAL_HEIGHT = FRAME_HEIGHT + ANTENNA_HEIGHT;

        List<Figure> list; // 만든 그림들을 TV처럼 보이게 모아줄 리스트

        // Tv를 켠다는 팝업을 클릭했을 때
        public void pressPowerButton()
        {
            _screen.setFill(); // screen인 box 그림 채우기
            _powerButton.setFill(); // powerbutton인 box 그림 채우기
        }
        // 안테나의 플래그에 따라 그릴지 말지 결정
        public void setAntenna()
        {
            if (_antennaFlag == true) // 안 그리는 경우
            {
                _antenna1 = null;
                _antenna2 = null;
            }
            else // 그리는 경우
            {
                int cx = _x1 + TOTAL_WIDTH / 2;
                int x1 = cx - ANTENNA_WIDTH;
                int y1 = _y1;
                int x2 = cx;
                int y2 = _y1 + ANTENNA_HEIGHT;
                _antenna1 = new Line(_popup, x1, y1, x2, y2);
                x1 = cx + ANTENNA_WIDTH;
                _antenna2 = new Line(_popup, x1, y1, x2, y2);
            }
            _antennaFlag = !_antennaFlag; // 안테나 플래그 TRUE->FALSE, FALSE->TRUE로
        }

        // 스크린의 색만 세팅하기 위한 함수
        public override void setColor(Color color)
        {
            base.setColor(color);
            _screen.setColor(color); // 파워 버튼은 색 변경 안 하도록
        }
        public override void draw(Graphics g, Pen p) // TV를 그릴 함수
        {
            /* // 아래는 Delegation 위임이다
            _frame.draw(g, p);
            _screen.draw(g, p);
            _channelButton.draw(g, p);
            _volumnButton.draw(g, p);
            _menuButton.draw(g, p);
            _powerButton.draw(g, p);
             * */
            foreach (Figure ptr in list) // 그림 객체가 모인 리스트 목록의 모든 걸 그리게
            {
                ptr.draw(g, p);
            }
            if (_antenna1 != null) // 안테나가 있으면 안테나를 그린다
            {
                _antenna1.draw(g, p);
            }
            if (_antenna2 != null)
            {
                _antenna2.draw(g, p);
            }
        }

        // 그려진 TV 를 옮길 때 사용할 함수
        public override void move(int dx, int dy)
        {
            base.move(dx, dy);
            /*
            _frame.move(dx, dy);
            _screen.move(dx, dy);
            _channelButton.move(dx, dy);
            _volumnButton.move(dx, dy);
            _menuButton.move(dx, dy);
            _powerButton.move(dx, dy);
             * */
            foreach (Figure ptr in list) // 역시 리스트를 열거해서 싹 다 옮긴다
            {
                ptr.move(dx, dy);
            }
            if (_antenna1 != null) // 안테나가 존재하면 안테나도 옮길 수 있게
            {
                _antenna1.move(dx, dy);
            }
            if (_antenna2 != null)
            {
                _antenna2.move(dx, dy);
            }
        }
        public override void draw(IntPtr hdc) // Rubber banding으로 그리기 위한 함수
        {
            /*
            _frame.draw(hdc);
            _screen.draw(hdc);
            _channelButton.draw(hdc);
            _volumnButton.draw(hdc);
            _menuButton.draw(hdc);
            _powerButton.draw(hdc);
             * */
            foreach (Figure ptr in list) // tv 그리기
            {
                ptr.draw(hdc);
            }
            if (_antenna1 != null) // 안테나 그리기
            {
                _antenna1.draw(hdc);
            }
            if (_antenna2 != null)
            {
                _antenna2.draw(hdc);
            }
        }
        
        // 그려진 TV를 복사하기 위한 함수
        public override Figure clone()
        {
            TV newFigure = new TV(_popup, _x1, _y1);
            newFigure.setColor(_color);
            if (_antennaFlag == false)
            {
                newFigure.setAntenna();
            }
            return newFigure;
        }

        // 클래스 이름을 돌려주는 함수
        public override String getClassName()
        {
            return "TV";
        }
    }

    
}
