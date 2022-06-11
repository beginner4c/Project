using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 다른 그림들과 다르게 TV 그림 위에서의 팝업을 띄워주기 위해 만든 클래스

namespace _24_XDrawer
{
    class TVPopup : FigurePopup
    {
        // Operations
        public TVPopup(XDrawer view) // constructor
            : base(view, "TV", false)
        {
            MenuItem powerItem = new MenuItem("ON/OFF");
            powerItem.Click += new EventHandler(view.onOnOffTv);
            _popupPtr.MenuItems.Add(powerItem);

            MenuItem antennaItem = new MenuItem("안테나 조작");
            antennaItem.Click += new EventHandler(view.onSetAntenna);
            _popupPtr.MenuItems.Add(antennaItem);
        }
    }
}
