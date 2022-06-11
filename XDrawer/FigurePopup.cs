using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24_XDrawer
{
    public class FigurePopup : Popup
    {
        public FigurePopup(XDrawer view, String title, bool fillFlag) // constructor
            : base(view, title)
        {
            MenuItem deleteItem = new MenuItem(" 지우기 "); // 메뉴아이템 객체 생성
            deleteItem.Click += new EventHandler(view.deleteFigure); // 지우기를 누르면 지우는 함수 호출
            _popupPtr.MenuItems.Add(deleteItem); // 메뉴 아이템 팝업에 추가

            MenuItem copyItem = new MenuItem(" 복사하기 ");
            copyItem.Click += new EventHandler(view.copyFigure);
            _popupPtr.MenuItems.Add(copyItem);

            MenuItem[] colorPopup = new MenuItem[4]; // 팝업메뉴 안에 팝업 메뉴가 더 추가되는 아이템 객체
            colorPopup[0] = new MenuItem(" 검정색 "); // 색 표기
            colorPopup[0].Click += new EventHandler(view.setBlackColor); // 
            colorPopup[1] = new MenuItem(" 빨강색 ");
            colorPopup[1].Click += new EventHandler(view.setRedColor);
            colorPopup[2] = new MenuItem(" 초록색 ");
            colorPopup[2].Click += new EventHandler(view.setGreenColor);
            colorPopup[3] = new MenuItem(" 파랑색 ");
            colorPopup[3].Click += new EventHandler(view.setBlueColor);

            _popupPtr.MenuItems.Add(" 색 정하기 ", colorPopup); // 팝업에 제목을 색 정하기로 해서 아이템 추가

            // 색을 채울 수 있는 그림이라면 채우기 추가
            if (fillFlag == true)
            {
                MenuItem fillItem = new MenuItem(" 채우기 ");
                fillItem.Click += new EventHandler(view.setFill);
                _popupPtr.MenuItems.Add(fillItem);
            }
        }
    }
}
