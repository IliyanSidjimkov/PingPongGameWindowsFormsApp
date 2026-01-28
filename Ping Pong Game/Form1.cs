namespace Ping_Pong_Game
{


    //Iliyan Sidzhimkov     
    //F112803
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        string currentLanguage = "EN";
        int ballXspeed = 4;
        int ballYspeed = 4;
        int speed = 2;
        Random rand = new Random();
        bool goDown, goUp;
        int computerSpeedChange = 50;
        int playerScore = 0;
        int computerScore = 0;
        int playerSpeed = 8;
        int[] i = { 5, 6, 8, 9 };
        int[] j = { 10, 9, 8, 11, 12 };


        private void GameTimerEvent(object sender, EventArgs e)
        {

            ball.Top -= ballYspeed;
            ball.Left -= ballXspeed;

            if (currentLanguage == "BG")
            {
                this.Text = $"Играч: {playerScore} --- Компютър: {computerScore}";
            }
            else
            {
                this.Text = $"Player: {playerScore} --- Computer: {computerScore}";
            }
            if (ball.Top < 0 || ball.Bottom > this.ClientSize.Height)
            {
                ballYspeed = -ballYspeed;

            }
            if (ball.Left < -2)
            {
                ball.Left = 300;
                ballXspeed = -ballXspeed;
                computerScore++;
            }
            if (ball.Right > this.ClientSize.Width + 2)
            {
                ball.Left = 300;
                ballXspeed = -ballXspeed;
                playerScore++;
            }
            if (computer.Top <= 1)
            {
                computer.Top = 0;

            }
            else if (computer.Bottom >= this.ClientSize.Height)
            {
                computer.Top = this.ClientSize.Height - computer.Height;
            }

            if (ball.Top < computer.Top + (computer.Height / 2) && ball.Left > 300)
            {
                computer.Top -= speed;
            }
            if (ball.Top > computer.Top + (computer.Height / 2) && ball.Left > 300)
            {
                computer.Top += speed;

            }

            computerSpeedChange -= 1;

            if (computerSpeedChange < 0)
            {
                speed = i[rand.Next(i.Length)];
                computerSpeedChange = 50;
            }
            if (goDown && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += playerSpeed;
            }
            if (goUp && player.Top > 0)
            {
                player.Top -= playerSpeed;
            }
            CheckCollison(ball, player, player.Right + 5);
            CheckCollison(ball, computer, computer.Left - 35);
            if (computerScore > 1)
            {
                GameOver(false);
            }
            else if (playerScore > 1)
            {
                GameOver(true);
            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
        }


        private void CheckCollison(PictureBox PicOne, PictureBox PicTwo, int offset)
        {

            if (PicOne.Bounds.IntersectsWith(PicTwo.Bounds))
            {
                PicOne.Left = offset;
                int x = j[rand.Next(j.Length)];
                int y = j[rand.Next(j.Length)];
                if (ballXspeed < 0)
                {
                    ballXspeed = x;

                }
                else
                {
                    ballXspeed = -x;
                }

                if (ballYspeed < 0)
                {
                    ballYspeed = -y;
                }
                else
                {
                    ballYspeed = -y;
                }
            }

        }


        private void GameOver(bool PlayerWon)
        {
            GameTimer.Stop();

            string message = "";
            string title = "";

            if (currentLanguage == "BG")
            {
                title = "Край на играта";
                message = PlayerWon
                    ? "Ти спечели играта!"
                    : "Съжаляваме, загуби играта.";
            }
            else
            {
                title = "Game Over";
                message = PlayerWon
                    ? "You won this game!"
                    : "Sorry, you lost the game.";
            }

            MessageBox.Show(message, title);

            computerScore = 0;
            playerScore = 0;
            ballXspeed = ballYspeed = 4;
            computerSpeedChange = 50;
            GameTimer.Start();

        }

        private void bulgarianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentLanguage = "BG";
            languageToolStripMenuItem.Text = "Език";
            bulgarianToolStripMenuItem.Text = "Български"; 
            englishToolStripMenuItem.Text = "Английски";
            aboutToolStripMenuItem.Text = "Относно";
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentLanguage = "EN";
            languageToolStripMenuItem.Text = "Language";
            bulgarianToolStripMenuItem.Text = "Bulgarian";
            englishToolStripMenuItem.Text = "English";
            aboutToolStripMenuItem.Text = "About";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }
        private void ShowAbout()
        {
            GameTimer.Stop();
            string title = "";
            string message = "";

            if (currentLanguage == "BG")
            {
                title = "Относно играта";
                message =
                    "Име на проекта: Pong Game\n" +
                    "Тип: Windows Forms Application\n\n" +
                    "Разработчик: Илиян Сиджимков\n" +
                    "Факултетен №: Ф112803\n" +
                    "Класическа Pong игра с управление от клавиатурата " +
                    "и поддръжка на български и английски език.";
            }
            else // EN
            {
                title = "About the game";
                message =
                    "Project name: Pong Game\n" +
                    "Type: Windows Forms Application\n\n" +
                    "Developer: Iliyan Sidzhimkov\n" +
                    "Faculty number: F112803\n" +
                    "Classic Pong game with keyboard control " +
                    "and support for Bulgarian and English language.";
            }
            
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            GameTimer.Start();
        }
    }
}
