using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24_XDrawer
{
    class MainPopup : Popup
    {
        public MainPopup(XDrawer view) // constructor
            : base(view, "Figure")
        {
            MenuItem BoxItem = new MenuItem("Box"); // 팝업에 추가할 아이템 생성
            _popupPtr.MenuItems.Add(BoxItem); // 팝업에 아이템 추가
            MenuItem LineItem = new MenuItem("Line");
            _popupPtr.MenuItems.Add(LineItem);
            MenuItem CircleItem = new MenuItem("Circle");
            _popupPtr.MenuItems.Add(CircleItem);
            MenuItem PointItem = new MenuItem("Point");
            _popupPtr.MenuItems.Add(PointItem);
            MenuItem KiteItem = new MenuItem("Kite");
            _popupPtr.MenuItems.Add(KiteItem);
            MenuItem tvItem = new MenuItem("TV");
            _popupPtr.MenuItems.Add(tvItem);

            // event handler 등록
            BoxItem.Click += new EventHandler(view.BoxToolStripMenuItem_Click); // 팝업 메뉴에서 Box를 클릭했을 때 발생하는 이벤트를 추가시켜 줌
            LineItem.Click += new EventHandler(view.LineToolStripMenuItem_Click); // 팝업 메뉴에서 Line을 클릭했을 때 발생하는 이벤트를 추가시켜 줌
            CircleItem.Click += new EventHandler(view.CircleToolStripMenuItem_Click); // 팝업 메뉴에서 Circle을 클릭했을 때 발생하는 이벤트를 추가시켜 줌
            PointItem.Click += new EventHandler(view.PointToolStripMenuItem_Click); // 팝업 메뉴에서 Point을 클릭했을 때 발생하는 이벤트를 추가시켜 줌
            KiteItem.Click += new EventHandler(view.KiteToolStripMenuItem_Click); // 팝업 메뉴에서 Kite을 클릭했을 때 발생하는 이벤트를 추가시켜 줌
            tvItem.Click += new EventHandler(view.TvToolStripMenuItem_Click); // 팝업 메뉴에서 TV을 클릭했을 때 발생하는 이벤트를 추가시켜 줌
        }
    }
}
