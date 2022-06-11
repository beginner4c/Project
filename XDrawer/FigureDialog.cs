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
    public partial class FigureDialog : Form
    {
        XDrawer mainForm; // 메인 폼을 불러올 객체
        static String[] figureTypes = { "Box", "Line", "Circle", "Point", "Kite", "TV" };

        public FigureDialog(XDrawer form) // 생성 시에 메인 폼을 가져옴
        {
            mainForm = form; // 메인 폼 저장
            InitializeComponent();

            textX1.Text = "0"; // 초기 화면에 좌표값 0을 넣어주게
            textX2.Text = "0"; // 초기 화면에 좌표값 0을 넣어주게
            textY1.Text = "0"; // 초기 화면에 좌표값 0을 넣어주게
            textY2.Text = "0"; // 초기 화면에 좌표값 0을 넣어주게

            for (int i = 0; i < figureTypes.Length; i++)
                selectBox.Items.Add(figureTypes[i]); // 콤보 박스에 아이템 추가

            /* // 아래 작업을 위의 for문으로 처리
            selectBox.Items.Add("Box"); // 콤보 박스에 아이템 추가
            selectBox.Items.Add("Line");
            selectBox.Items.Add("CIrcle");
            selectBox.Items.Add("Point");
            selectBox.Items.Add("Kite");
            selectBox.Items.Add("TV");
            */

            selectBox.SelectedIndex = 0; // 콤보박스에 첫 번째 아이템을 초기 화면에 보여주게 한다

            redButton.ForeColor = Color.Red; // 라디오 버튼 글자색을 변경
            greenButton.ForeColor = Color.Green;
            blueButton.ForeColor = Color.Blue;

            // 컨트롤에 마우스를 갖다대면 설명을 더해주는 객체
            ToolTip tip = new ToolTip();
            tip.SetToolTip(blackButton, "Black Color"); // radiobutton에 설명 달기
            tip.SetToolTip(redButton, "Red Color");
            tip.SetToolTip(greenButton, "Green Color");
            tip.SetToolTip(blueButton, "Blue Color");

            blackButton.Select(); // 라디오 버튼 기본값을 검정으로 설정
            mainForm.CurrentColor = Color.Black; // 색의 기본값 세팅을 검정으로
        }

        // ok 버튼
        private void OkButton_Click(object sender, EventArgs e)
        {
            // Text에 아무 값도 안들어있는 경우 발생하는 NullPointException을 방지하기 위한 곳
            if (textX1.Text.Length == 0)
                return;
            if (textX2.Text.Length == 0)
                return;
            if (textY1.Text.Length == 0)
                return;
            if (textY2.Text.Length == 0)
                return;

            //MessageBox.Show("" +selectBox.SelectedIndex); 확인용

            int x1 = int.Parse(textX1.Text);
            int x2 = int.Parse(textX2.Text);
            int y1 = int.Parse(textY1.Text);
            int y2 = int.Parse(textY2.Text);

            // dialog를 통해서 status strip에 현재 그릴 도형 이름에 대해 표시하기 위한 설정
            mainForm.setFigureTypeLabel(figureTypes[selectBox.SelectedIndex]);

            // dynamic binding으로 인해 많은 if문을 줄일 수 있지만 객체를 처음 만들 땐 어쩔 수 없다
            Figure newFigure = null;
            // upcasting
            if (selectBox.SelectedIndex == XDrawer.DRAW_BOX - 1)
            {
                newFigure = new Box(mainForm.boxPopup, x1, y1, x2, y2);
                // mainForm.setFigureTypeLabel("Box"); // dialog에서 도형을 선택했을 때 status strip에서 글자를 바꾸기 위해서 설정
            }
            else if(selectBox.SelectedIndex == XDrawer.DRAW_LINE - 1)
            {
                newFigure = new Line(mainForm.linePopup, x1, y1, x2, y2);
                // mainForm.setFigureTypeLabel("Line");
            }
            else if(selectBox.SelectedIndex == XDrawer.DRAW_CIRCLE - 1)
            {
                newFigure = new Circle(mainForm.circlePopup, x1, y1, x2, y2);
                // mainForm.setFigureTypeLabel("Circle");
            }
            else if(selectBox.SelectedIndex == XDrawer.DRAW_POINT - 1)
            {
                newFigure = new Point(mainForm.pointPopup, x1, y1);
                // mainForm.setFigureTypeLabel("Point");
            }
            else if (selectBox.SelectedIndex == XDrawer.DRAW_KITE - 1)
            {
                newFigure = new Kite(mainForm.pointPopup, x1, y1, x2, y2);
                // mainForm.setFigureTypeLabel("Point");
            }
            else if (selectBox.SelectedIndex == XDrawer.DRAW_TV - 1)
            {
                newFigure = new TV(mainForm.tvPopup, x1, y1);
            }
            newFigure.setColor(mainForm.CurrentColor); // radio button을 통해 변경된 색 적용
            mainForm.addFigure(newFigure);
            // 한 번 그리고 나면 사라지게
            // Hide();
        }

        // cancle 버튼
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Hide(); // 다이얼로그 화면을 사라지게하는 함수 호출
        }

        private void SelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender; // 콤보박스처럼 사용할 수 있는 객체 생성

            // MessageBox.Show(""+box.SelectedIndex); // selectedindex가 정수라 "" + 로 처리, 확인용
        }

        // radio button을 클릭했을 때 발생하는 이벤트
        private void BlackButton_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.CurrentColor = Color.Black; // XDrawer의 property를 통해 색을 세팅
        }

        private void RedButton_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.CurrentColor = Color.Red;
        }

        private void GreenButton_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.CurrentColor = Color.Green;
        }

        private void BlueButton_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.CurrentColor = Color.Blue;
        }

        // 버튼을 눌렀을 때 color dialog를 이용할 수 있게 한다
        private void SelectColorButton_Click(object sender, EventArgs e)
        {
            // color dialog 객체 생성
            ColorDialog dialog = new ColorDialog();
            
            // dialog 사용 중 OK 버튼을 눌렀을 때 인식하는 부분
            if (dialog.ShowDialog() == DialogResult.OK) // modal dialog로 color dialog를 불러와서 ok 버튼이 눌려졌을 때
            {
                mainForm.CurrentColor = dialog.Color; // color dialog에서 선택한 색을 그릴 색으로 지정
            }
        }
    }
}
