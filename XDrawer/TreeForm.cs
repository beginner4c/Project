using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24_XDrawer
{
    public partial class TreeForm : Form
    {
        public TreeForm(XDrawer mainForm) // constructor
        {
            InitializeComponent();
            this.mainForm = mainForm;
            treeView.Dock = DockStyle.Fill; // 폼 크기를 늘이면 자동으로 맞게 늘어나게 한다
            this.TopMost = true; // modaless dialog로 활용되는 것도 화면이 맨 위에 위치할 수 있음

            treeView.Nodes.Add("Figures"); // root 노드의 이름을 Figures로 지정
            treeView.Nodes[0].Nodes.Add("Box"); // root 노드 밑에 Box 노드 추가
            treeView.Nodes[0].Nodes.Add("Line"); // root 노드 밑에 Line 노드 추가
            treeView.Nodes[0].Nodes.Add("Circle"); // root 노드 밑에 Circle 노드 추가
            treeView.Nodes[0].Nodes.Add("Point"); // root 노드 밑에 Point 노드 추가
            treeView.Nodes[0].Nodes.Add("Kite"); // Kite 추가
            treeView.Nodes[0].Nodes.Add("TV"); // TV 추가

            showAllFigures();
        }

        // data member
        XDrawer mainForm;

        // TreeView를 실제적으로 구현할 함수
        void showAllFigures()
        {
            TreeNodeCollection nodesToAdd = null; // 내가 집어넣어야할 컬렉션의 위치값을 가져올 포인터
            List<Figure> figures = mainForm.Figures; // XDrawer에 있는 Figure List를 가져온다

            foreach(Figure ptr in figures) // 가져온 리스트를 돌아본다
            {
                // RTTI Run Time Type Identification
                if (ptr is Kite)
                {
                    // 주의 - Box가 Kite의 부모 클래스이기 때문에 
                    // Kite를 먼저 물어봐야 함
                    // 안그러면 Kite가 Box 취급 당함
                    nodesToAdd = treeView.Nodes[0].Nodes[4].Nodes;
                }
                else if (ptr is Box) // ptr이 Box class 객체인지 확인
                {
                    nodesToAdd = treeView.Nodes[0].Nodes[0].Nodes; // child node 를 집어넣을 위치를 nodesToAdd 포인터에 할당
                    /*
                    // down casting, 위험한 행위로 RTTI를 통해서 이루어질 수 있게 하자
                    Box p = (Box)ptr;                    
                    */

                }
                else if(ptr is Line)
                {
                    nodesToAdd = treeView.Nodes[0].Nodes[1].Nodes; 
                }
                else if(ptr is Circle)
                {
                    nodesToAdd = treeView.Nodes[0].Nodes[2].Nodes;
                }
                else if (ptr is Point)
                {
                    nodesToAdd = treeView.Nodes[0].Nodes[3].Nodes;
                }
                else if (ptr is TV)
                {
                    nodesToAdd = treeView.Nodes[0].Nodes[5].Nodes;
                }

                // 좌표값 가져오기
                int x1 = ptr.getX1();
                int y1 = ptr.getY1();
                int x2 = ptr.getX2();
                int y2 = ptr.getY2();

                // TreeView에서 표시할 문자열 생성
                String s = "(" + x1 + ", " + y1 + ")";

                if(x2>=0 && y2 >= 0) // TwoPointFigure의 경우 (getX,Y 함수 default 값은 -1)
                {
                    s += " / (" + x2 + ", " + y2 + ")";
                }

                // child 노드에 문자열 추가
                nodesToAdd.Add(s);
            }
        }
    }
}
