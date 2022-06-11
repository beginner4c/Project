using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24_XDrawer
{
    public partial class XDrawer : Form
    {
        // DATA MEMBER
        public static int DRAW_BOX = 1;
        public static int DRAW_LINE = 2;
        public static int DRAW_CIRCLE = 3;
        public static int DRAW_POINT = 4;
        public static int DRAW_KITE = 5;
        public static int DRAW_TV = 6;

        // RUBBER BANDING을 위한 R2 값
        static int R2_NOTXORPEN = 10; // 무조건적으로 10으로 지정해야 한다

        static int NOTHING = 0; // 아무 동작도 아닐 때
        static int DRAWING = 1;
        static int MOVING = 2;

        Color _currentColor;

        // Figure List를 넘겨주는 TreeForm에서 사용할 함수
        public List<Figure> Figures
        {
            get // property로 처리
            {
                return _figures;
            }
        }

        // _currentColor 객체를 건드리는 property
        public Color CurrentColor
        {
            get
            {
                return _currentColor;
            }
            set
            {
                _currentColor = value; // property 만들 때 사용하는 reserved word 예약어
            }
        }

        public Popup mainPopup = null; // 팝업 객체
        public Popup pointPopup = null;
        public Popup boxPopup = null;
        public Popup linePopup = null;
        public Popup circlePopup = null;
        public Popup kitePopup = null;
        public Popup tvPopup = null;

        Figure _selectedFigure; // 그림을 그릴 객체
        List<Figure> _figures; // 그림들을 저장할 리스트

        private int _whatToDraw; // 어떤 그림을 그릴지 정하기 위한 정수
        private int _actionMode; // 마우스가 움직일 때 그림을 그릴지, 움직일지 그냥인지 구분용
        private int _currentX;
        private int _currentY;
        private String _fileName; // 파일의 이름을 담을 문자열

        public PictureBox Canvas // PictureBox 를 돌려줄 property
        {
            get
            {
                return canvas;
            }
        }

        public XDrawer() // 생성자 constructor
        {
            InitializeComponent();
            _figures = new List<Figure>();
            _whatToDraw = DRAW_BOX; // 초기화 시 0으로 해주면 오류가 발생하기 때문에 생성 시 BOX를 초기값으로
            _currentColor = Color.Black; // 색 초기화
            _actionMode = NOTHING; // 마우스 모드는 암 것도 아닌 걸로
            _selectedFigure = null; // 그릴 도형 초기화
            _fileName = ""; // 파일 이름 초기화

            mainPopup = new MainPopup(this); // canvas가 아닌 Form 전체를 넘긴다
            boxPopup = new FigurePopup(this, "Box", true); // Form과 이름, 색 채우기를 할건지 결정
            linePopup = new FigurePopup(this, "Line", false);
            circlePopup = new FigurePopup(this, "Circle", true);
            pointPopup = new FigurePopup(this, "Point", false);
            kitePopup = new FigurePopup(this, "Kite", false);
            tvPopup = new TVPopup(this); // TVPopup 클래스에 이미 이름과 채우기가 넘어가 있다

            // 이미지를 지정할 땐 실행 파일 위치 혹은 full path name으로 할당
            toolStripBlackButton.Image = Image.FromFile("Black.png"); // 툴 스트립의 검정색의 이미지 설정
            toolStripRedButton.Image = Image.FromFile("Red.png"); // 툴 스트립의 검정색의 이미지 설정
            toolStripGreenButton.Image = Image.FromFile("Green.png"); // 툴 스트립의 검정색의 이미지 설정
            toolStripBlueButton.Image = Image.FromFile("Blue.png"); // 툴 스트립의 검정색의 이미지 설정

            _currentX = 0;
            _currentY = 0;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            // 마우스 오른쪽을 누를 시
            if (e.Button == MouseButtons.Right)
            {
                // 그림을 잡는 걸 초기화
                _selectedFigure = null;
                foreach(Figure ptr in _figures) // 만들어진 그림들의 리스트를 순회하면서
                {
                    if(ptr.ptInRegion(e.X, e.Y)) // 지정된 위치에 그림이 있으면
                    {
                        _selectedFigure = ptr; // 해당 그림을 잡고 빠져나간다
                        break;
                    }
                }

                if(_selectedFigure != null) // 그림 잡은 게 있으면
                {   // 이와 같이 자식 클래스에 작업을 넘기는 걸 위임한다고 한다 Delegation
                    _selectedFigure.popup(e.Location); // 해당 region의 클래스에 맞는 popup 호출
                }
                else // 그림 잡은 게 없으면
                {
                    mainPopup.popup(e.Location); // 기본 메인 팝업을 열어준다
                }

                return;
            }
            // 아래는 마우스 위치가 Figure의 Region 안에 있는지 확인해서 그림을 이동할 수 있게 하는 곳이다
            _selectedFigure = null;
            foreach (Figure ptr in _figures) // 만들어진 그림들의 리스트를 순회하면서
            {
                if (ptr.ptInRegion(e.X, e.Y)) // 지정된 위치에 그림이 있으면
                {
                    _selectedFigure = ptr; // 해당 그림을 잡고 빠져나간다
                    break;
                }
            }
            if(_selectedFigure != null)
            {
                _actionMode = MOVING; // 모드를 그림 이동으로
                _figures.Remove(_selectedFigure); // 이동할 그림을 리스트에서 제거
                _currentX = e.X; _currentY = e.Y; // 현재 마우스 좌표 저장
                return;
            }

            // 마우스 왼쪽을 누를 시
            if (_whatToDraw == DRAW_BOX)
            {
                // upcasting ,박스 그리기
                _selectedFigure = new Box(boxPopup, e.X, e.Y); // 마우스를 누를 때마다 새로운 객체 생성
            }
            else if (_whatToDraw == DRAW_LINE)
            {
                _selectedFigure = new Line(linePopup, e.X, e.Y); // 객체 생성 시 x, y 좌표값을 현재 마우스 위치로 초기화
            }
            else if (_whatToDraw == DRAW_CIRCLE)
            {
                _selectedFigure = new Circle(circlePopup, e.X, e.Y);
            }
            else if(_whatToDraw == DRAW_POINT)
            {
                _selectedFigure = new Point(pointPopup, e.X, e.Y);
            }
            else if(_whatToDraw == DRAW_KITE)
            {
                _selectedFigure = new Kite(kitePopup, e.X, e.Y);
            }
            else if(_whatToDraw == DRAW_TV)
            {
                _selectedFigure = new TV(tvPopup, e.X, e.Y);
            }

            _selectedFigure.setColor(_currentColor); // 그림 그리는 객체 색상 지정
            _actionMode = DRAWING;
        }

        // 마우스 오른쪽 버튼을 누르건 왼쪽 버튼을 누르건 이벤트 핸들러는 똑같다
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = canvas.CreateGraphics(); // canvas 객체 안에서 사용하는 그래픽스 객체 생성
            Pen pen = new Pen(Color.Black);

            // dynamic binding
            _selectedFigure.draw(g, pen);

            // C#은 Automatic garbage collection을 채택했으나 완벽하지 않기 때문에 큰 객체를 호출하면 따로 처리해주어야 한다
            g.Dispose(); // garbage collection을 위해서 사용

            _selectedFigure.makeRegion(); // 그림을 완성하면서 region을 만든다 -> 나중에 popup 등에 사용하려고

            // 이전엔 마우스를 누르자 말자 배열에 더해줬지만 이제는 완성시킨 후 리스트에 넣어준다
            _figures.Add(_selectedFigure);

            _selectedFigure = null;
            _actionMode = NOTHING;

            canvas.Invalidate(); // 화면을 무효화 함으로써 paint 이벤트를 다시 호출하게 한다
        }

        // modal dialog에서 OK 버튼을 눌렀을 때 list에 좌표를 넣을 수 있게 해주는 함수
        public void addFigure(Figure fig) // 여기서 Figure를 사용할 수 있게 Figure class에서 public으로 설정해주어야 함
        {
            // 다이얼로그를 통해 그림을 만들 때도 region을 가지고 있어야 한다
            fig.makeRegion(); // region 잡기

            _figures.Add(fig); // 그림 리스트에 그림 추가

            canvas.Invalidate(); // 넣었으니 페인트 재호출
        }

        // Rubber Banding을 위한 C의 DLL을 로드하는 곳
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern int SetROP2(IntPtr hdc, int rop2);

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            PositionLabel.Text = "" + e.X + ", " + e.Y; // 마우스의 움직임에 따라 status strip에 마우스 좌표가 표시된다

            // region이 적용되는 범위를 알기 위해서 마우스 포인터 모양을 바꾸는 곳
            bool inFigure = false;

            foreach (Figure ptr in _figures)
            {
                if (ptr.ptInRegion(e.X, e.Y)) // 마우스 좌표가 그려진 그림 위인 경우
                {
                    inFigure = true;
                    break;
                }
            }
            if (inFigure == true)
            {
                Cursor = Cursors.Cross;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            if (_actionMode == NOTHING)
                return;

            int newX = e.X;
            int newY = e.Y;

            // 기존에는 Graphics 객체 g를 통해 그림을 그렸지만 이제는 C의 Device Context Handle을 이용해 그린다
            Graphics g = canvas.CreateGraphics(); // canvas 객체 안에서 사용하는 그래픽스 객체 생성
            IntPtr hdc = g.GetHdc();
            int oldMode = SetROP2(hdc, R2_NOTXORPEN); // SetROP2는 C의 Win32에서 가져온다, 이를 통해 rubber banding을 구현

            // rubber banding을 통해 그리고 이동할 수 있게
            if (_actionMode == DRAWING)
            {
                _selectedFigure.drawing(hdc, newX, newY);
            }
            else if (_actionMode == MOVING)
            {
                _selectedFigure.move(hdc, newX - _currentX, newY - _currentY);
                _currentX = newX;
                _currentY = newY;
            }

            SetROP2(hdc, oldMode);

            g.ReleaseHdc(hdc); // 사용한 HDC는 돌려줘야 한다

            // C#은 Automatic garbage collection을 채택했으나 완벽하지 않기 때문에 큰 객체를 호출하면 따로 처리해주어야 한다
            g.Dispose(); // garbage collection을 위해서 사용
        }

        // 여기 페인트를 invalidate로 호출하게 되면 각 클래스의 draw 함수가 재호출된다
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            // clip area를 자동으로 넘겨주니까 e에서 갖다 쓰면 된다
            // 이 경우 새로운 그래픽스를 만든 것이 아니니까 dispose를 하면 안된다
            Graphics g = e.Graphics; // 그래픽스 객체

            Pen pen = new Pen(Color.Black); // 펜 객체

            // Paint 이벤트가 호출되었을 때 그려놨던 그림을 표시하기 위해 사용
            foreach (Figure ptr in _figures)
            {
                ptr.draw(g, pen);
            }
        }

        // status strip에서 그릴 도형 이름을 보여주기 위한 함수
        public void setFigureTypeLabel(String s)
        {
            figureTypeLabel.Text = s;
        }

        // 스튜디오가 만들어준 클래스라도 디자이너 말고 여기 거는 수정해도 문제가 없다
        // 메인 창 스트립 메뉴에서 클릭을 관리하는 이벤트 핸들러들
        public void BoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _whatToDraw = DRAW_BOX;
            setFigureTypeLabel("Box");
        }

        public void LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _whatToDraw = DRAW_LINE;
            setFigureTypeLabel("Line");
        }

        public void CircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _whatToDraw = DRAW_CIRCLE;
            setFigureTypeLabel("Circle");
        }

        public void PointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _whatToDraw = DRAW_POINT;
            setFigureTypeLabel("Point");
        }

        public void KiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _whatToDraw = DRAW_KITE;
            setFigureTypeLabel("Kite");
        }

        public void TvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _whatToDraw = DRAW_TV;
            setFigureTypeLabel("TV");
        }

        private void ModalDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FigureDialog dlg = new FigureDialog(this);

            dlg.ShowDialog(); // 내부에서 무한루프 비스무리하게 돌아가면서 dialog 외부에서 일어나는 이벤트를 전부 무시
        }

        private void ModalessDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FigureDialog dlg = new FigureDialog(this);

            dlg.Show(); // ShowDialog와 다르게 외부 이벤트도 받아준다
        }

        // 직접 만든 이벤트 핸들러
        public void deleteFigure(object sender, EventArgs e) // 선택된 그림을 지우는 함수
        {
            // MessageBox.Show("hello"); // 호출되는지 확인용
            if (_selectedFigure == null) // 선택된 그림이 없다면 (빈 화면에 마우스 우클릭 시에)
                return;

            // region은 Figure 클래스를 통해 만들어지게 되어있으므로 해당 Figure가 사라지면 자동으로 삭제
            _figures.Remove(_selectedFigure); // 그림들을 저장한 리스트에서 선택된 그림을 제거하는 함수 호출

            canvas.Invalidate(); // paint 재호출
        }

        // Figure class 내부의 setcolor 함수를 호출할 함수
        private void setColor(Color color)
        {
            if (_selectedFigure == null) // 만약 선택된 Figure가 없으면 리턴
                return;

            _selectedFigure.setColor(color); // 

            canvas.Invalidate(); // canvas의 paint 이벤트 재호출
        }

        // popup을 통해서 색을 정하면 그 색을 세팅하는 이벤트 핸들러들
        public void setBlackColor(object sender, EventArgs e) // 검은색 함수
        {
            setColor(Color.Black);
        }
        public void setRedColor(object sender, EventArgs e) // 빨간색 함수
        {
            setColor(Color.Red);
        }
        public void setBlueColor(object sender, EventArgs e) // 파란색 함수
        {
            setColor(Color.Blue);
        }
        public void setGreenColor(object sender, EventArgs e) // 초록색 함수
        {
            setColor(Color.Green);
        }
        // 팝업에서 채우기를 누르면 그림의 색을 채우는 이벤트 핸들러
        public void setFill(object sender, EventArgs e)
        {
            if (_selectedFigure == null) // 선택된 그림이 없는 경우
                return;

            _selectedFigure.setFill(); // Figure class의 setFill 함수 호출해서 그림을 채우지 않았다면 채우고 채웠다면 선으로만 되돌릴 수 있게 한다

            canvas.Invalidate(); // canvas paint 재호출해서 리스트의 Figure들을 draw할 수 있게
        }

        // 팝업에서 복사하기를 누르면 그림을 복사하는 이벤트 핸들러
        public void copyFigure(object sender, EventArgs e)
        {
            if (_selectedFigure == null) // 선택된 그림이 없는 경우
                return;

            Figure newFigure = _selectedFigure.clone(); // 현재 선택된 그림을 복사하는 함수를 호출해 새 Figure 만들기
            newFigure.move(10, 20); // 복사된 그림이 겹치지 않게 좌표를 조금 이동시키는 함수 호출

            addFigure(newFigure); // addFigure 함수에서 리스트 추가, region 처리, invalidate를 한 번에 처리해주니까 사용
        }

        // tool strip의 버튼을 눌렀을 때
        private void ToolStripBlackButton_Click(object sender, EventArgs e)
        {
            _currentColor = Color.Black; // 맞는 색상 지정
        }

        private void ToolStripRedButton_Click(object sender, EventArgs e)
        {
            _currentColor = Color.Red;
        }

        private void ToolStripGreenButton_Click(object sender, EventArgs e)
        {
            _currentColor = Color.Green;
        }

        private void ToolStripBlueButton_Click(object sender, EventArgs e)
        {
            _currentColor = Color.Blue;
        }

        private void TreeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeForm tree = new TreeForm(this);
            tree.ShowDialog(); // modal dialog로 보인다
        }

        private void TableViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListForm table = new ListForm(this);
            table.ShowDialog(); // modal dialog 호출
        }

        // 그린 페이지를 출력하는 이벤트 핸들러
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog dialog = new PrintDialog(); // print에 사용하는 dialog 객체 생성
            PrintDocument document = new PrintDocument(); // 이게 있어야 출력이 가능하다

            document.PrintPage += new PrintPageEventHandler(doPrint);
            dialog.Document = document;

            if(dialog.ShowDialog() == DialogResult.OK) // modal dialog 호출해 확인 버튼을 누를 때
            {
                document.Print(); // Print 함수를 통해 Invalidate 함수와 비슷한 효과로 doPrint에 접근할 수 있다
            }
        }

        // PrintPageEventHandler에 parameter로 사용할 함수
        private void doPrint(object sender, PrintPageEventArgs ev) // document의 Print 함수를 통해서 parameter에 값이 설정된다
        {
            Graphics g = ev.Graphics; // 용지에 출력할 수 있는 Graphics 객체를 생성

            Pen pen = new Pen(Color.Black); // 펜 객체

            // Paint 이벤트가 호출되었을 때 그려놨던 그림을 표시하기 위해 사용
            foreach (Figure ptr in _figures)
            {
                ptr.draw(g, pen); // 종이에 그려질 그림들을 그린다
            }
        }
        // 그린 그림을 출력 전 미리보기 할 수 있는 이벤트 핸들러
        private void PrintPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dialog = new PrintPreviewDialog(); // 미리보기에 사용하는 dialog 객체 생성
            PrintDocument document = new PrintDocument(); // 이게 있어야 출력이 가능하다

            document.PrintPage += new PrintPageEventHandler(doPrint);
            dialog.Document = document;

            dialog.ShowDialog(); // modal dialog로 호출

            /*
            if (dialog.ShowDialog() == DialogResult.OK) // modal dialog 호출해 확인 버튼을 누를 때
            {
                document.Print(); // Print 함수를 통해 Invalidate 함수와 비슷한 효과로 doPrint에 접근할 수 있다
            }
            */
        }

        public void onOnOffTv(object sender, EventArgs e)
        {
            if (_selectedFigure == null) return;
            // 이 작업을 위해서는 RTTI를 이용해 TV 인가를 확인해야 한다.
            if (_selectedFigure is TV)
            {
                TV pTV = (TV)_selectedFigure;
                pTV.pressPowerButton();
            }
            canvas.Invalidate();
        }

        public void onSetAntenna(object sender, EventArgs e)
        {
            if (_selectedFigure == null) return;
            // 이 작업을 위해서는 RTTI를 이용해 TV 인가를 확인해야 한다.
            if (_selectedFigure is TV)
            {
                TV pTV = (TV)_selectedFigure;
                pTV.setAntenna();
            }
            canvas.Invalidate();
        }

        // Save를 누르면 호출되는 이벤트 핸들러
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog chooser = new SaveFileDialog(); // save에 사용하는 dialog 객체 생성
            chooser.Title = "File Save"; // dialog 제목 지정
            chooser.Filter = "XDrawer Files (*.xdr) | *.xdr"; // 저장되는 파일 형식 지정
            chooser.InitialDirectory = "C:\\Users\\user\\Desktop"; // 초기에 호출되는 파일 위치를 현재 위치로
            chooser.OverwritePrompt = true; // 이미 있는 파일의 경우 경고창을 보일 수 있게

            if (chooser.ShowDialog() != DialogResult.OK) // dialog 결과 이벤트가 ok가 아니면 return
                return;

            String fileName = chooser.FileName; // 파일 이름을 dialog에서 지정해주는 path를 포함한 이름으로 지정

            if (fileName == null) // 파일 이름이 비어있으면 return
                return;

            if (fileName.Length == 0) // 파일 이름 길이가 0이어도 return
                return;

            _fileName = fileName; // 위의 조건을 거친 fileName을 다시 저장

            doSave();

            // MessageBox.Show(fileName); // 파일 이름 확인용
            // chooser.ShowDialog(); // dialog 호출
        }

        // 파일 이름을 기준으로 저장하는 함수
        private void doSave()
        {
            BinaryFormatter formatter = new BinaryFormatter(); // 파일을 이진수로 바꿔주는 객체 생성
            Stream output = File.Create(_fileName); // 출력하기 위해 파일을 만든 객체

            // 아래 작업을 위해서 저장할 모든 정보들에 대해 Serializable을 명시해주어야 한다
            formatter.Serialize(output, _figures); // _figures 리스트의 모든 정보를 output이라는 stream에 출력해라

            output.Close(); // 작업이 끝나면 닫아주기
        }

        // Open을 누르면 호출되는 이벤트 핸들러
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog chooser = new OpenFileDialog(); // 파일을 열어주는 dialog 생성
            chooser.Title = "File Open"; // 제목 설정
            chooser.Filter = "XDrawer Files (*.xdr)|*.xdr"; // 파일 이름을 xdr로 지정
            chooser.InitialDirectory = "C:\\Users\\user\\Desktop"; // dialog가 참조할 위치 지정

            if (chooser.ShowDialog() != DialogResult.OK) // dialog의 ok버튼 호출이 아니면 return
                return;

            // 아래는 초기화 작업
            _fileName = chooser.FileName;
            _figures.Clear();
            _selectedFigure = null;

            BinaryFormatter formatter = new BinaryFormatter(); // 파일을 이진수로 바꿔주는 객체
            Stream input = File.OpenRead(_fileName); // 입력받을 곳

            _figures = (List<Figure>)formatter.Deserialize(input); // input을 거꾸로 읽어들여서 Figure의 리스트 객체로 만들어라

            input.Close(); // 작업이 끝나면 닫아준다

            // List를 전부 돌면서 세팅해주는 작업, 리스트만 만들면 그림만 생성될 뿐 Region과 맞는 Popup이 잡히지 않기 때문에 하는 작업이다
            foreach (Figure ptr in _figures)
            {
                ptr.makeRegion(); // 범위를 잡게 해주는 함수 호출
                
                // RTTI를 통해서 어떤 클래스로 만들어졌는지 확인
                if (ptr is Point)
                {
                    ptr.setPopup(pointPopup);
                }
                else if (ptr is Kite) // Box를 상속받았기 때문에 Box보다 먼저 확인해줘야 한다
                {
                    ptr.setPopup(kitePopup);
                }
                else if (ptr is Box)
                {
                    ptr.setPopup(boxPopup);
                }
                else if (ptr is Line)
                {
                    ptr.setPopup(linePopup);
                }
                else if (ptr is Circle)
                {
                    ptr.setPopup(circlePopup);
                }
                else if (ptr is TV)
                {
                    ptr.setPopup(tvPopup);
                }
            }

            canvas.Invalidate(); // paint 이벤트를 재호출
        }

        // New를 누르면 호출되는 이벤트 핸들러
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 기존의 그림들을 지우고 깔끔한 새 화면을 만들 수 있게 한다
            _figures.Clear(); // Figure List 초기화
            _selectedFigure = null; // 선택된 그림 없게
            canvas.Invalidate(); // paint 이벤트 재호출
        }
    }
}
