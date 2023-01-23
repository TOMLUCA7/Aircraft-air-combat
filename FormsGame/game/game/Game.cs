using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Game : Form
    {
        bool goLeft, goRight, shooting, isGameOver;
        int score;
        int playerSpeed = 35;
        int enemySpeed = 10;
        int bulletSpeed = 25;
        Random rnd = new Random();

        public Game()
        {
            InitializeComponent();
            resetGame();
        }

        private void KeyGoDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void KeyGoUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            // מתי שלוחצים על מקש רווח כדי לירות
            if (e.KeyCode == Keys.Space && shooting == false)
            {
                shooting = true;

                bullet.Top = player.Top - 30; // מהירות של הירי
                bullet.Left = player.Left + (player.Width / 2); // חישוב של אמצע התמונה 

            }

            // מתי שהשחקן ניפסל לוחצים על מקש אנטר כדי להתחיל משחק חדש
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            lblScore.Text = score.ToString();

            // המהירויות של האויבים הוזזה שלהם למטע
            enemyOne.Top += enemySpeed;
            enemyTwo.Top += enemySpeed;
            enemyThree.Top += enemySpeed;

            // אם האויב עובר את את אורך המסך המשחק ניגמר 
            if (enemyOne.Top > 550 || enemyTwo.Top > 550 || enemyThree.Top > 550)
            {
                gameOver();
            }

            // שהשחקן לא יעבור את רוחב המשך מצד שמאל
            if (goLeft == true && player.Left > 2)
            {
                player.Left -= playerSpeed;
            }

            // שהשחקן לא יעבור את אורך המסך מצד ימין
            if (goRight == true && player.Left < 765)
            {
                player.Left += playerSpeed;
            }

            // מתי שלוחצים על מקש רווח המשתנה עובר לאמת והירי מתבצע והתמונה של הירי עולה לראש המסך אם לא יורים המשתנה נישאר בלא נכון והתמונה של ירי לא מופיעה על המסך
            if (shooting == true)
            {
                bulletSpeed = 25;
                bullet.Top -= bulletSpeed;
            }
            else
            {
                bullet.Left = -300;
                bulletSpeed = 0;
            }

            if (bullet.Top < -30)
            {
                shooting = false;
            }

            // אם התמונה של הירי פןעגה ברוחב של התמונה של האויב מעברים את התמונה למחוץ המסך מעלימים את התמונה של הירי ומקבלים ניקוד
            if (bullet.Bounds.IntersectsWith(enemyOne.Bounds))
            {
                score += 1;
                enemyOne.Top = -450;
                enemyOne.Left = rnd.Next(20, 600);
                shooting = false;
            }
            if (bullet.Bounds.IntersectsWith(enemyTwo.Bounds))
            {
                score += 1;
                enemyTwo.Top = -650;
                enemyTwo.Left = rnd.Next(20, 600);
                shooting = false;
            }
            if (bullet.Bounds.IntersectsWith(enemyThree.Bounds))
            {
                score += 1;
                enemyThree.Top = -750;
                enemyThree.Left = rnd.Next(20, 600);
                shooting = false;
            }


            // מתי שהשחקן מגיעה לניקוד מסויים המהירות של האויב עולה
            if (score == 5)
            {
                enemySpeed = 15;
            }
            if (score == 10)
            {
                enemySpeed = 19;
            }
            if (score == 15)
            {
                enemySpeed = 22;
            }
        }


        private void resetGame()
        {
            // הפעלה שך התיימר
            GameTimer.Start();


            // מהירות רנדומלית של האוייב 
            enemyOne.Left = rnd.Next(20, 600);
            enemyTwo.Left = rnd.Next(20, 600);
            enemyThree.Left = rnd.Next(20, 600);


            // מיקום התחלה של האוייב שהמיקום שלו יתחיל מחוץ למסך
            // אם מקבלים במספר רנדומלי 100 ואז מכפילים אותו במינוס 1 מקבלים מספר שלילי מה שאומר שבאוייב יקבל מיקום חדש מחוץ למסך
            enemyOne.Top = rnd.Next(0, 200) * -1;
            enemyTwo.Top = rnd.Next(0, 500) * -1;
            enemyThree.Top = rnd.Next(0, 900) * -1;

            score = 0;
            bulletSpeed = 0;
            bullet.Left = -300;
            shooting = false;


            lblScore.Text = score.ToString();

        }

        private void gameOver()
        {
            isGameOver = true;
            GameTimer.Stop();
            lblScore.Text += " - Game Over!! \n Press Enter to try again.";
        }

    }
}
