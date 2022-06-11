using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24_XDrawer
{
    public class Popup
    {
        public Popup(XDrawer view, String title) // constructor
        {
            _pView = view; // canvas
            _popupPtr = new ContextMenu(); // popup 객체

            if (title != null)
            {
                _popupPtr.MenuItems.Add(title); // 팝업에 제목 추가
                _popupPtr.MenuItems.Add("-"); // 제목 아래 실선(separator) 추가
            }
        }

        // data member
        protected ContextMenu _popupPtr; // 우클릭 popup을 만드는 객체
        protected XDrawer _pView; // canvas 객체를 받을 거

        public void popup(System.Drawing.Point pos) // 팝업을 보이는 함수
        {
            _popupPtr.Show(_pView.Canvas, pos); // 캔버스를 기준으로 좌표에 팝업 호출
        }
    }
}
