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
    public partial class ListForm : Form
    {
        public ListForm(XDrawer mainForm) // constructor, 외관 완성
        {
            InitializeComponent();
            this.mainForm = mainForm;

            // listView 속성 정의
            listView.Parent = this; // ListForm이 listView의 부모라고 명시
            listView.Dock = DockStyle.Fill; // 폼 크기를 늘이면 자동으로 맞게 늘어나게 한다
            listView.View = View.Details; // 보기 형식을 자세히 보기로 지정
            listView.MultiSelect = false; // listView의 다중 선택 기능을 막는다

            // ListForm 속성 정의
            this.TopMost = true; // modaless dialog로 활용되는 것도 화면이 맨 위에 위치할 수 있음

            this.SetBounds(100, 100, 370, 400); // 윈도우의 100,100 좌표 기준으로 370, 400 크기의 ListForm 세팅

            listView.Columns.Add("Figure", 70, HorizontalAlignment.Center); // 열을 추가하는데 Figure라는 글자를 70너비만큼 중간에다가 적어라
            listView.Columns.Add("x1", 70, HorizontalAlignment.Center); // 열을 추가하는데 Figure라는 글자를 70너비만큼 중간에다가 적어라
            listView.Columns.Add("y1", 70, HorizontalAlignment.Center); // 열을 추가하는데 Figure라는 글자를 70너비만큼 중간에다가 적어라
            listView.Columns.Add("x2", 70, HorizontalAlignment.Center); // 열을 추가하는데 Figure라는 글자를 70너비만큼 중간에다가 적어라
            listView.Columns.Add("y2", 70, HorizontalAlignment.Center); // 열을 추가하는데 Figure라는 글자를 70너비만큼 중간에다가 적어라

            // 디자인에서 만들지 않고 직접 코딩을 통해서 panel과 그 안의 button 컨트롤 생성
            Panel panel = new Panel();
            panel.Dock = DockStyle.Bottom; // Form 밑바닥에 Panel을 위치하게 함
            panel.Parent = this; // ListForm이 panel의 부모라고 선언

            String[] buttonName = { "Detail", "List", "Tile", "Cancel" }; // 버튼 이름들을 저장
            for(int i = 0; i<buttonName.Length; i++)
            {
                button[i] = new Button(); // 버튼 초기화
                button[i].Text = buttonName[i]; // 버튼 텍스트를 문자열 배열로 넣어줌
                button[i].SetBounds(10 + 65 * i, 20, 60, 40); // 버튼 위치를 10+55*i,20 으로 잡고 너비 높이를 60 40으로
                button[i].Click += new EventHandler(buttonClicked); // 버튼 클릭 이벤트 핸들러
                panel.Controls.Add(button[i]); // panel에 버튼을 컨트롤로 추가해줌
            }

            showAllFigures(); // 실제적으로 ListView 내용 만들기
        }

        // data member
        XDrawer mainForm;
        Button[] button = new Button[4]; // 버튼 4개 배열 생성

        // panel 안의 button의 이벤트 핸들러
        void buttonClicked(Object sender, EventArgs e)
        {
            // 원래 RTTI를 통해 Down Casting 혹은 defensive programming을 해주어야 한다
            if (!(sender is Button))
                return;
            Button button = (Button)sender; // sender가 눌러진 버튼을 의미한다

            String name = button.Text; // 버튼의 이름을 가져온다
            // 가져온 이름을 통해 비교
            if (name.Equals("Detail")) // 눌려진 버튼의 이름이 Detail인 경우
            {
                listView.View = View.Details; // listView를 detail로 세팅
            }
            else if (name.Equals("List")) // 눌려진 버튼의 이름이 List인 경우
            {
                listView.View = View.List; // listView를 List로 보여주게
            }
            else if (name.Equals("Tile")) // 눌려진 버튼의 이름이 Tile인 경우
            {
                listView.View = View.Tile; // listView를 Tile로 보여주게
            }
            else if (name.Equals("Cancel")) // 취소하면 창 감추기
            {
                Hide();
            }
        }

        // ListView를 실제적으로 구현할 함수
        void showAllFigures()
        {
            List<Figure> figures = mainForm.Figures; // XDrawer에 있는 Figure List를 가져온다

            foreach (Figure ptr in figures) // 가져온 리스트를 돌아본다
            {
                // 아이템 객체 생성
                ListViewItem item = new ListViewItem(ptr.getClassName()); // dynamic binding을 통한 if문 삭제

                /* dynamic biding을 통해 RTTI if 문 축소
                // RTTI Run Time Type Identification
                if (ptr is Box) // ptr이 Box class 객체인지 확인
                {
                    item = new ListViewItem("Box"); // constructor에 문자열을 주기 위한 RTTI
                }
                else if (ptr is Line)
                {
                    item = new ListViewItem("Line");
                }
                else if (ptr is Circle)
                {
                    item = new ListViewItem("Circle");
                }
                else if (ptr is Point)
                {
                    item = new ListViewItem("Point");
                }
                */

                // 좌표값 가져오기
                int x1 = ptr.getX1();
                int y1 = ptr.getY1();
                int x2 = ptr.getX2();
                int y2 = ptr.getY2();

                // 가져온 클래스 이름에 맞추어 좌표값 추가
                item.SubItems.Add("" + x1); // 2열에 x1 좌표
                item.SubItems.Add("" + y1); // 3열에 y1 좌표

                if (x2 >= 0 && y2 >= 0) // TwoPointFigure의 경우 (getX,Y 함수 default 값은 -1)
                {
                    item.SubItems.Add("" + x2); // 4열에 x2 좌표
                    item.SubItems.Add("" + y2); // 5열에 y2 좌표
                }

                listView.Items.Add(item); // 정리된 아이템을 listView에 추가

                // TreeView에서 표시할 문자열 생성
                String s = "(" + x1 + ", " + y1 + ")";

                if (x2 >= 0 && y2 >= 0) // TwoPointFigure의 경우 (getX,Y 함수 default 값은 -1)
                {
                    s += " / (" + x2 + ", " + y2 + ")";
                }
            }
        }
    }
}
