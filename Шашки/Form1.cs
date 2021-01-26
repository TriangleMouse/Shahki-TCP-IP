using Шашки.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace Шашки
{
    public partial class Form1 : Form
    {

        const int mapSize = 8;
        const int cellSize = 60;

        int currentPlayer;
        int player = 2;
        List<PictureBox> simpleSteps = new List<PictureBox>();

        int countEatSteps = 0;
        PictureBox prevButton;
        PictureBox pressedButton;
        bool isContinue = false;

        bool isMoving;

        int[,] map = new int[mapSize, mapSize];

        PictureBox[,] pictures = new PictureBox[mapSize, mapSize];

        Image whiteFigure;
        Image blueFigure;


        public Form1()
        {
            // При выходе из приложения не забудьте сначала отключить его
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
            OnServer.Value = false;
            whiteFigure = Resources.white;
            blueFigure = Resources.Blue;


            this.Text = "Checkers";

            Init();
        }

        public void Init()
        {

            currentPlayer = 1;
            isMoving = false;
            prevButton = null;

            map = new int[mapSize, mapSize] {
                { 0,1,0,1,0,1,0,1 },
                { 1,0,1,0,1,0,1,0 },
                { 0,1,0,1,0,1,0,1 },
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 2,0,2,0,2,0,2,0 },
                { 0,2,0,2,0,2,0,2 },
                { 2,0,2,0,2,0,2,0 }
            };

            CreateMap();
        }
       
        public void ResetGame()
        {
            bool player1 = false;
            bool player2 = false;


            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == 1)
                        player1 = true;
       
                    if (map[i, j] == 2)
                        player2 = true;
                }
            }
            if (!player1 || !player2)
            {
                this.Controls.Clear();
                InitializeComponent();
                Init();
            }
        }
        PictureBox picture;
        public void CreateMap()
        {
            //this.Width = (mapSize + 1) * cellSize;
            //this.Height = (mapSize + 1) * cellSize;






            this.ClientSize = new System.Drawing.Size(949, 480);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                     picture = new PictureBox();

                    picture.Location = new Point(j * cellSize, i * cellSize);

                    picture.Size = new Size(cellSize, cellSize);
                    picture.Click += new EventHandler(OnFigurePress);
                   
                    if (map[i, j] == 1)
                    {
                        picture.Image = whiteFigure;
                        picture.SizeMode = PictureBoxSizeMode.CenterImage;

                    }

                    else if (map[i, j] == 2)
                    {
                        picture.Image = blueFigure;
                        picture.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                    picture.BackColor = GetPrevButtonColor(picture);
                    picture.ForeColor = Color.Red;
                    picture.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictures[i, j] = picture;

                    this.Controls.Add(picture);
                }
            }
        }

        public void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 1 ? 2 : 1;

            ResetGame();
        }

        public Color GetPrevButtonColor(PictureBox prevButton)
        {
            if ((prevButton.Location.Y / cellSize % 2) != 0)
            {
                if ((prevButton.Location.X / cellSize % 2) == 0)
                {
                    return Color.FromArgb(0, 0, 0);
                }
            }
            if ((prevButton.Location.Y / cellSize) % 2 == 0)
            {
                if ((prevButton.Location.X / cellSize) % 2 != 0)
                {
                    return Color.FromArgb(0, 0, 0);
                }
            }
            return Color.FromArgb(128, 0, 0);
        }
        string str = "";
        bool flag = false;
        public void OnFigurePress(object sender, EventArgs e)
        {
            if (flag)
            {
                if (prevButton != null)
                    prevButton.BackColor = GetPrevButtonColor(prevButton);

                pressedButton = sender as PictureBox;

                if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] != 0 && (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == currentPlayer && player == currentPlayer))
                {
                    CloseSteps();
                    pressedButton.BackColor = Color.Red;
                    DeactivateAllButtons();
                    pressedButton.Enabled = true;
                    countEatSteps = 0;
                    if (pressedButton.Text == "D")
                        ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
                    else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);

                    if (isMoving)
                    {
                        CloseSteps();
                        pressedButton.BackColor = GetPrevButtonColor(pressedButton);
                        ShowPossibleSteps();
                        isMoving = false;
                    }
                    else
                        isMoving = true;



                }
                else
                {
                    if (isMoving)
                    {
                        isContinue = false;
                        if (Math.Abs(pressedButton.Location.X / cellSize - prevButton.Location.X / cellSize) > 1)
                        {
                            isContinue = true;
                            DeleteEaten(pressedButton, prevButton);

                        }

                        //string [] a = str.Split(' ');
                        //if (a[0] != "Delet")
                        str = "mov" + " ";
                        str += Convert.ToString(pressedButton.Location.Y / cellSize) + " ";
                        str += Convert.ToString(pressedButton.Location.X / cellSize) + " ";
                        str += Convert.ToString(prevButton.Location.Y / cellSize) + " ";
                        str += Convert.ToString(prevButton.Location.X / cellSize) + " ";
                      
                        int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                        map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];

                        map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = temp;
                        pressedButton.Image = prevButton.Image;
                        prevButton.Image = null;
                        pressedButton.Text = prevButton.Text;
                        prevButton.Text = "";
                        SwitchButtonToCheat(pressedButton);
                        countEatSteps = 0;
                        isMoving = false;
                        CloseSteps();
                        DeactivateAllButtons();
                        if (pressedButton.Text == "D")
                            ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
                        else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);
                        if (countEatSteps == 0 || !isContinue)
                        {
                            CloseSteps();
                            SwitchPlayer();
                            ShowPossibleSteps();
                            isContinue = false;
                        }
                        else if (isContinue)
                        {
                            pressedButton.BackColor = Color.Red;
                            pressedButton.Enabled = true;
                            isMoving = true;
                        }
                        str += Convert.ToString(currentPlayer);
                        SendMessageMap(str);
                        str = "";
                    }

                }

                prevButton = pressedButton;
            }
        }
        private void SendMessageMap(string txtMessage)
        {
           
                swSender.WriteLine(txtMessage); ;
                swSender.Flush();
            
        }

        public void ShowPossibleSteps()
        {
            bool isOneStep = true;
            bool isEatStep = false;
            DeactivateAllButtons();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == currentPlayer)
                    {
                        if (pictures[i, j].Text == "D")
                            isOneStep = false;
                        else isOneStep = true;
                        if (IsButtonHasEatStep(i, j, isOneStep, new int[2] { 0, 0 }))
                        {
                            isEatStep = true;
                            pictures[i, j].Enabled = true;
                        }
                    }
                }
            }
            if (!isEatStep)
                ActivateAllButtons();
        }

        public void SwitchButtonToCheat(PictureBox picture)
        {
            if (map[picture.Location.Y / cellSize, picture.Location.X / cellSize] == 1 && picture.Location.Y / cellSize == mapSize - 1)
            {
                picture.Text = "D";
                picture.Image = Resources.whiteDamka;

            }
            if (map[picture.Location.Y / cellSize, picture.Location.X / cellSize] == 2 && picture.Location.Y / cellSize == 0)
            {
                picture.Text = "D";
                picture.Image = Resources.blueDamka;

            }
        }

        public void DeleteEaten(PictureBox endButton, PictureBox startButton)
        {
            int count = Math.Abs(endButton.Location.Y / cellSize - startButton.Location.Y / cellSize);
            int startIndexX = endButton.Location.Y / cellSize - startButton.Location.Y / cellSize;
            int startIndexY = endButton.Location.X / cellSize - startButton.Location.X / cellSize;
            startIndexX = startIndexX < 0 ? -1 : 1;
            startIndexY = startIndexY < 0 ? -1 : 1;
            int currCount = 0;
            int i = startButton.Location.Y / cellSize + startIndexX;
            int j = startButton.Location.X / cellSize + startIndexY;
            while (currCount < count - 1)
            {
                //str = "Delet ";
                //str += i + " ";
                //str += j + " ";
                map[i, j] = 0;
                pictures[i, j].Image = null;
                pictures[i, j].Text = "";
                i += startIndexX;
                j += startIndexY;
                currCount++;
            }

        }

        public void ShowSteps(int iCurrFigure, int jCurrFigure, bool isOnestep = true)
        {
            simpleSteps.Clear();
            ShowDiagonal(iCurrFigure, jCurrFigure, isOnestep);
            if (countEatSteps > 0)
                CloseSimpleSteps(simpleSteps);
        }

        public void ShowDiagonal(int IcurrFigure, int JcurrFigure, bool isOneStep = false)
        {
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
        }

        public bool DeterminePath(int ti, int tj)
        {

            if (map[ti, tj] == 0 && !isContinue)
            {
                pictures[ti, tj].BackColor = Color.Yellow;
                pictures[ti, tj].Enabled = true;
                simpleSteps.Add(pictures[ti, tj]);
            }
            else
            {

                if (map[ti, tj] != currentPlayer)
                {
                    if (pressedButton.Text == "D")
                        ShowProceduralEat(ti, tj, false);
                    else ShowProceduralEat(ti, tj);
                }

                return false;
            }
            return true;
        }

        public void CloseSimpleSteps(List<PictureBox> simpleSteps)
        {
            if (simpleSteps.Count > 0)
            {
                for (int i = 0; i < simpleSteps.Count; i++)
                {
                    simpleSteps[i].BackColor = GetPrevButtonColor(simpleSteps[i]);
                    simpleSteps[i].Enabled = false;
                }
            }
        }
        public void ShowProceduralEat(int i, int j, bool isOneStep = true)
        {
            int dirX = i - pressedButton.Location.Y / cellSize;
            int dirY = j - pressedButton.Location.X / cellSize;
            dirX = dirX < 0 ? -1 : 1;
            dirY = dirY < 0 ? -1 : 1;
            int il = i;
            int jl = j;
            bool isEmpty = true;
            while (IsInsideBorders(il, jl))
            {
                if (map[il, jl] != 0 && map[il, jl] != currentPlayer)
                {
                    isEmpty = false;
                    break;
                }
                il += dirX;
                jl += dirY;

                if (isOneStep)
                    break;
            }
            if (isEmpty)
                return;
            List<PictureBox> toClose = new List<PictureBox>();
            bool closeSimple = false;
            int ik = il + dirX;
            int jk = jl + dirY;
            while (IsInsideBorders(ik, jk))
            {
                if (map[ik, jk] == 0)
                {
                    if (IsButtonHasEatStep(ik, jk, isOneStep, new int[2] { dirX, dirY }))
                    {
                        closeSimple = true;
                    }
                    else
                    {
                        toClose.Add(pictures[ik, jk]);
                    }
                    pictures[ik, jk].BackColor = Color.Yellow;
                    pictures[ik, jk].Enabled = true;
                    countEatSteps++;
                }
                else break;
                if (isOneStep)
                    break;
                jk += dirY;
                ik += dirX;
            }
            if (closeSimple && toClose.Count > 0)
            {
                CloseSimpleSteps(toClose);
            }

        }

        public bool IsButtonHasEatStep(int IcurrFigure, int JcurrFigure, bool isOneStep, int[] dir)
        {
            bool eatStep = false;
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (dir[0] == 1 && dir[1] == -1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j + 1))
                            eatStep = false;
                        else if (map[i - 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (dir[0] == 1 && dir[1] == 1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            eatStep = false;
                        else if (map[i - 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (dir[0] == -1 && dir[1] == 1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j - 1))
                            eatStep = false;
                        else if (map[i + 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (dir[0] == -1 && dir[1] == -1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j + 1))
                            eatStep = false;
                        else if (map[i + 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
            return eatStep;
        }

        public void CloseSteps()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    pictures[i, j].BackColor = GetPrevButtonColor(pictures[i, j]);
                }
            }
        }

        public bool IsInsideBorders(int ti, int tj)
        {
            if (ti >= mapSize || tj >= mapSize || ti < 0 || tj < 0)
            {
                return false;
            }
            return true;
        }

        public void ActivateAllButtons()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    pictures[i, j].Enabled = true;
                }
            }
        }

        public void DeactivateAllButtons()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    pictures[i, j].Enabled = false;
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        // Будет содержать имя пользователя
        private string UserName = "Unknown";
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;
        //Необходимо обновить форму сообщениями из другого потока
   private delegate void UpdateLogCallback(string strMessage);
        // Необходимо установить форму в "отключенное" состояние от другого потока
        private delegate void CloseConnectionCallback(string strReason);
        private Thread thrMessaging;
        private IPAddress ipAddr;
        private bool Connected;

        public int port { get; set; }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Connected == false)
            {
                // Инициализировать подключение
                InitializeConnection();
            }
            else
            {
                CloseConnection("Disconnected at user's request.");
            }
        }


        // Обработчик событий для выхода из приложения
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected == true)
            {
                // Закрывает соединения, потоки и т. д.
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
            }
        }

        private void InitializeConnection()
        {
            // Проанализируйте IP-адрес из текстового поля в объект IPAddress
            ipAddr = IPAddress.Parse(txtIp.Text);



            port = Convert.ToInt32(Port.Text);
            // Запуск нового TCP соединения с сервером чата
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddr, port);

            // Помогает нам отслеживать, связаны мы или нет
            Connected = true;
            // Подготовьте форму
            UserName = txtUser.Text;

            // Отключите и включите соответствующие поля
            txtIp.Enabled = false;
            txtUser.Enabled = false;
            txtMessage.Enabled = true;
            btnSend.Enabled = true;
            btnConnect.Text = "Disconnect";

            // Отправьте нужное имя пользователя на сервер
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(txtUser.Text);
            swSender.Flush();

            // Запустите поток для получения сообщений и дальнейшего общения
            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();
        }

        private void ReceiveMessages()
        {
            //Получите ответ от сервера
            srReceiver = new StreamReader(tcpServer.GetStream());
            //Если первый символ ответа равен 1, то соединение прошло успешно
            string ConResponse = srReceiver.ReadLine();
            // Если первый символ равен 1, то соединение прошло успешно
            if (ConResponse[0] == '1')
            {
                // Обновите форму, чтобы сообщить ему, что теперь мы подключены
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
            }
            else // Если первый символ не является 1 (вероятно, 0), соединение было неудачным
            {
                string Reason = "Not Connected: ";
                // Извлеките причину из ответного сообщения. Причина начинается с 3-го символа
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                // Обновите форму с указанием причины, по которой мы не смогли подключиться
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                // Exit the method
                return;
            }
            // Пока мы успешно подключены, считывайте входящие строки с сервера
            while (Connected)
            {
                // Show the messages in the log TextBox
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
            }
        }

        // Этот метод вызывается из другого потока для обновления журнала TextBox
        private void UpdateLog(string strMessage)
        {
            string[] str = strMessage.Split(' ');
            if (str[0] != "mov" && str[0] != "Delet")
            {
                // Добавление текста также прокручивает текстовое поле вниз каждый раз
                txtLog.AppendText(strMessage + "\r\n");
                flag = true;
            }
            else
            {
           
                    prevButton = pictures[int.Parse(str[3]), int.Parse(str[4])];

              //  currentPlayer = int.Parse(str[5]);
                    Mov(pictures[int.Parse(str[1]), int.Parse(str[2])], str);
      
                
            }
     
        }

        public void Mov(object sender, string[] str)
        {
            if (prevButton != null)
                prevButton.BackColor = GetPrevButtonColor(prevButton);

            pressedButton = sender as PictureBox;

          
                    isContinue = false;
                    if (Math.Abs(pressedButton.Location.X / cellSize - prevButton.Location.X / cellSize) > 1)
                    {
                        isContinue = true;
                        DeleteEaten(pressedButton, prevButton);
                    }


                    int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                    map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];

                    map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = temp;
                    pressedButton.Image = prevButton.Image;
                    prevButton.Image = null;
                    pressedButton.Text = prevButton.Text;
                    prevButton.Text = "";
                    SwitchButtonToCheat(pressedButton);
                    countEatSteps = 0;
                    isMoving = false;
                    CloseSteps();
                    DeactivateAllButtons();
            if (pressedButton.Text == "D")
                ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
            else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);
            if (countEatSteps == 0 || !isContinue)
            {
                CloseSteps();
                SwitchPlayer();
                ShowPossibleSteps();
                isContinue = false;
            }
            else if (isContinue)
            {
                pressedButton.BackColor = Color.Red;
                pressedButton.Enabled = true;
                isMoving = true;
            }

            //    }

            //}

            //prevButton = pressedButton;
        }
       // Closes a current connection
        private void CloseConnection(string Reason)
        {
            // Покажите причину, по которой соединение заканчивается
            txtLog.AppendText(Reason + "\r\n");
            // Включение и отключение соответствующих элементов управления в форме
            txtIp.Enabled = true;
            txtUser.Enabled = true;
            txtMessage.Enabled = false;
            btnSend.Enabled = false;
            btnConnect.Text = "Connect";

            // Закрываем обьекты
            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }

        // Отправляет набранное сообщение на сервер
        private void SendMessage()
        {
            if (txtMessage.Lines.Length >= 1)
            {
                swSender.WriteLine(txtMessage.Text);
                swSender.Flush();
                txtMessage.Lines = null;
            }
            txtMessage.Text = "";
        }

        // Мы хотим отправить сообщение при нажатии кнопки Отправить    
        private void btnSend_Click_1(object sender, EventArgs e)
        {
            SendMessage();
        }
        // Но мы также хотим отправить сообщение после нажатия клавиши Enter



        private void txtMessage_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // If the key is Enter
            if (e.KeyChar == (char)13)
            {
                SendMessage();
            }
        }

        private void OnServer_OnValueChange(object sender, EventArgs e)
        {
            if (OnServer.Value == true)
            {
                player = 1;
                // Считываем IP-адреса сервера из  TextBox
                IPAddress ipAddr = IPAddress.Parse(txtIp.Text);
                port = Convert.ToInt32(Port.Text);
                //Создаем новый экземпляр объекта ChatServer
                ChatServer mainServer = new ChatServer(ipAddr, port);
                // Подключаем обработчик событий StatusChanged к mainServer_StatusChanged
                ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
                //Начинаем  прослушивать соединения
                mainServer.StartListening();
                // Show that we started to listen for connections
                // txtLog.AppendText("Monitoring for connections...\r\n");
            }
        }


        private delegate void UpdateStatusCallback(string strMessage);


        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Вызываем метод, который обновляет форму
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
        }


        private void UpdateStatus(string strMessage)
        {
            // Обновляем журнал с сообщением
            txtLog.AppendText("\r");
        }




    }

}
