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
using System.Collections;

namespace Шашки
{
    // Содержит аргументы для события StatusChanged
    public class StatusChangedEventArgs : EventArgs
    {
        // Аргумент, который нас интересует, - это сообщение, описывающее событие
        private string EventMsg;

        // Свойство для получения и установки сообщения о событи
        public string EventMessage
        {
            get
            {
                return EventMsg;
            }
            set
            {
                EventMsg = value;
            }
        }

        // Конструктор для установки сообщения о событии
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }

    // Этот делегат необходим для указания параметров, которые мы передаем вместе с нашим событием
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    class ChatServer
    {
        public int Port { get; set; }
        // В этой хэш-таблице хранятся пользователи и соединения (просматриваемые пользователем)
        public static Hashtable htUsers = new Hashtable(2); // 2 пользователей за один лимит времени
        // В этой хэш-таблице хранятся соединения и пользователи (просматриваемые по соединению)
        public static Hashtable htConnections = new Hashtable(2); // 2 пользователей за один лимит времени
        // Will store the IP address passed to it
        private IPAddress ipAddress;
        private TcpClient tcpClient;
        // Событие и его аргумент будут уведомлять форму, когда пользователь подключился, отключился, отправил сообщение и т. д.
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;

        //Конструктор устанавливает IP-адрес на тот, который был получен создающим экземпляр объектом
        public ChatServer(IPAddress address, int port)
        {
            ipAddress = address;
            Port = port;
        }

        // Поток, который будет содержать прослушиватель соединения
        private Thread thrListener;

        // Объект TCP, который прослушивает соединения
        private TcpListener tlsClient;

        // Скажет циклу while, чтобы он продолжал следить за подключениями
        bool ServRunning = false;

        // Добавляет пользователя в хэш-таблицу
        public static void AddUser(TcpClient tcpUser, string strUsername)
        {
            // Сначала добавдяем имя пользователя и связанное с ним соединение в обе хэш таблицы
            ChatServer.htUsers.Add(strUsername, tcpUser);
            ChatServer.htConnections.Add(tcpUser, strUsername);

            // пишем  о новом соединении со всеми другими пользователями и с формой сервера
            SendAdminMessage(htConnections[tcpUser] + "присоединился к нам");
            if (htConnections[0] != "")
            {
               
            }
        }

        // Удаление пользователя из хэш-таблиц
        public static void RemoveUser(TcpClient tcpUser)
        {
            // Если пользователь находится в хэш-таблице
            if (htConnections[tcpUser] != null)
            {
                // Сначала показываем информацию и расскахываем другим пользователям об отключении
                SendAdminMessage(htConnections[tcpUser] + " покинул нас");

                // Удаляем пользователя из хэш-таблицы
                ChatServer.htUsers.Remove(ChatServer.htConnections[tcpUser]);
                ChatServer.htConnections.Remove(tcpUser);
            }
        }

        // Это вызывается, когда мы хотим вызвать событие StatusChanged
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                // Вызоваем наш  делегат
                statusHandler(null, e);
            }
        }

        // Отправка административных сообщений
        public static void SendAdminMessage(string Message)
        {
            StreamWriter swSenderSender;

            // Прежде всего, покажите в нашем приложении, кто что говорит
            //e = new StatusChangedEventArgs("Administrator: " + Message);
            //OnStatusChanged(e);

            // Создаем массив TCP-клиентов, размер которого соответствует количеству имеющихся у нас пользователей
            TcpClient[] tcpClients = new TcpClient[ChatServer.htUsers.Count];
            // Скопируем объекты TcpClient в массив
            ChatServer.htUsers.Values.CopyTo(tcpClients, 0);
            // Циклический просмотр списка TCP-клиентов
            for (int i = 0; i < tcpClients.Length; i++)
            {
                // Попробуем отправить сообщение каждому из них
                try
                {
                    // Если сообщение пустое или соединение нулевое, пропускаем
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    // Отправляем сообщение текущему пользователю в цикле
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine("Administrator: " + Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch // Если возникла проблема, то пользователя там больше нет, удалите его
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        // Отправляем сообщения от одного пользователя всем остальны
        public static void SendMessage(string From, string Message)
        {
            StreamWriter swSenderSender;

            // Прежде всего, покажем в нашем приложении, кто что говорит
            e = new StatusChangedEventArgs(From + " говорит: " + Message);
            OnStatusChanged(e);

            // Создаем массив TCP-клиентов, размер которого соответствует количеству имеющихся у нас пользователей
            
            TcpClient[] tcpClients = new TcpClient[ChatServer.htUsers.Count];
            int j =0;
            //Скопируем объекты TcpClient в массив 
            
           ChatServer.htUsers.Values.CopyTo(tcpClients, 0);
            
            // Циклический просмотр списка TCP-клиентов
            for (int i = 0; i < tcpClients.Length; i++)
            {
                // Попробуйте отправить сообщение каждому из них
                try
                {
                
                    // Если сообщение пустое или соединение нулевое, просто пропускаем
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                  
                    // Если сообщение пустое или соединение нулевое, вырывайтесь
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine(From + " говорит:  " + Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                { // Если возникла проблема, то пользователя там больше нет, удалите его
                    RemoveUser(tcpClients[i]);
                }
            }
        }
        public static void SendMessageMap(string From, string Message)
        {
            StreamWriter swSenderSender;

       

            TcpClient[] tcpClients = new TcpClient[ChatServer.htUsers.Count - 1];
            int j = 0;
            //Скопируем объекты TcpClient в массив 
            foreach (string client in ChatServer.htUsers.Keys)
            {
                if (client != From)
                {
                    tcpClients[j] = (TcpClient)ChatServer.htUsers[client];
                    j++;
                }
            }
  

            // Циклический просмотр списка TCP-клиентов
            for (int i = 0; i < tcpClients.Length; i++)
            {
                // Попробуйте отправить сообщение каждому из них
                try
                {

                 
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine(Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                { // Если возникла проблема, то пользователя там больше нет, удалите его
                    RemoveUser(tcpClients[i]);
                }
            }
        }
        public void StartListening()
        {

            // Получаем IP-адрес первого сетевого устройства, однако это может оказаться ненадежным в некоторых конфигурациях
            IPAddress ipaLocal = ipAddress;

            // Создаем объект прослушивателя TCP, используя IP-адрес сервера и указанный порт
            tlsClient = new TcpListener(Port);

            // Запускае прослушиватель TCP и прослушайте соединения
            tlsClient.Start();

            // Цикл while будет проверять значение true в этом случае перед проверкой соединений
            ServRunning = true;

            // Запускаем новый протектор, на котором размещается слушатель
            thrListener = new Thread(KeepListening);
            thrListener.Start();
        }

        private void KeepListening()
        {
            // Пока сервер работает
            while (ServRunning == true)
            {
                // Принимаем отложенное соединение
                tcpClient = tlsClient.AcceptTcpClient();
                // Создаем новый экземпляр соединения
                Connection newConnection = new Connection(tcpClient);
            }
        }
    }

    // Этот класс handels connections; его экземпляров будет столько же, сколько подключенных пользователей
    class Connection
    {
        TcpClient tcpClient;
        // Поток, который будет отправлять информацию клиенту
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currUser;
        private string strResponse;

        // Конструктор класса принимает в себя TCP соединение
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            // Поток, который принимает клиента и ожидает сообщений
            thrSender = new Thread(AcceptClient);
            // Поток вызывает метод AcceptClient() 
            thrSender.Start();
        }

        private void CloseConnection()
        {
            // Закрываем текущие открытые объекты
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }

        // Происходит, когда принимается новый клиент
        private void AcceptClient()
        {
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());

            // Читаем информацию о счете от клиента
            currUser = srReceiver.ReadLine();

            // Мы получили ответ от клиента
            if (currUser != "")
            {
                // Сохраняем имя пользователя в хэш-таблице
                if (ChatServer.htUsers.Contains(currUser) == true)
                {
                    // 0 означает не подключен
                    swSender.WriteLine("0|Это имя пользователя уже существует.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else if (currUser == "Administrator")
                {
                    // 0 означает не подключен
                    swSender.WriteLine("0|Это имя пользователя зарезервировано.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else
                {
                    // 1 означает успешное подключение
                    swSender.WriteLine("1");
                    swSender.Flush();

                    // Добавьте пользователя в хэш-таблицы и начните прослушивать сообщения от него
                    ChatServer.AddUser(tcpClient, currUser);
                }
            }
            else
            {
                CloseConnection();
                return;
            }

            try
            {
                // Продолжаем ждать сообщения от пользователя
                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    // Если он недействителен, удаляем пользователя
                    if (strResponse == null)
                    {
                        ChatServer.RemoveUser(tcpClient);
                    }
                    else
                    {
                        string[] str = strResponse.Split(' ');
                        if (str[0] != "mov" && str[0] != "Delet")
                        {
                            // В противном случае отправляем сообщение всем остальным пользователям
                            ChatServer.SendMessage(currUser, strResponse);
                        }
                        else
                        {
                            
                            ChatServer.SendMessageMap(currUser, strResponse);

                           
                        }
                    }
                }
            }
            catch
            {
                // Если что-то пошло не так с этим пользователем, отключите его
                ChatServer.RemoveUser(tcpClient);
            }
        }
    }
}

