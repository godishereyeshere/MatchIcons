using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1Game
{
    public partial class Form1 : Form
    {
        // متغیرهای مربوط به انتخاب دکمه‌ها
        Label firstClicked = null;
        Label secondClicked = null;

        // متغیر تصادفی برای انتخاب عناصر آیکون
        Random random = new Random();

        // لیست آیکون‌ها (هر آیکون دو بار تکرار شده است)
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        public Form1()
        {
            InitializeComponent();
            // فراخوانی تابع تخصیص آیکون به دکمه‌ها
            AssignIconsToSquares();
        }

        // تابع برای تخصیص آیکون به دکمه‌ها
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    // انتخاب یک عدد تصادفی برای آیکون
                    int randomNumber = random.Next(icons.Count);
                    // تخصیص آیکون به دکمه
                    iconLabel.Text = icons[randomNumber];
                    // تنظیم رنگ متن به همانند رنگ پس‌زمینه (برای مخفی کردن متن)
                    iconLabel.ForeColor = iconLabel.BackColor;
                    // حذف آیکون انتخاب شده از لیست
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        // رویداد کلیک بر روی دکمه‌ها
        private void label_Click(object sender, EventArgs e)
        {
            // اگر تایمر فعال باشد، عملیات کلیک نادیده گرفته می‌شود
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                // اگر متن دکمه سیاه باشد، عملیات کلیک نادیده گرفته می‌شود
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // اولین دکمه انتخاب شده
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // دومین دکمه انتخاب شده
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // بررسی برنده شدن
                CheckForWinner();

                // اگر آیکون‌ها یکسان نباشند، تایمر فعال می‌شود
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // فعال‌سازی تایمر
                timer1.Start();
            }
        }

        // تایمر برای مخفی کردن دو دکمه انتخاب شده
        private void timer1_Tick(object sender, EventArgs e)
        {
            // ایستادن تایمر
            timer1.Stop();
            // مخفی کردن دو دکمه انتخاب شده
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            // تنظیم متغیرهای انتخاب به مقدار null
            firstClicked = null;
            secondClicked = null;
        }

        // بررسی برنده شدن بازی
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    // اگر هنوز هم دکمه‌ای وجود داشته باشد که متنش مخفی نشده باشد، ادامه می‌دهد
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // اگر همه دکمه‌ها مخفی شده باشند، پیام برنده شدن نمایش داده می‌شود و برنامه بسته می‌شود
            MessageBox.Show("تمام آیکون‌ها را متناظر کردید! تبریک می‌گویم.", "تبریک");
            Close();
        }


        private void RestartGame()
        {
            // تنظیم متغیرها به حالت اولیه
            firstClicked = null;
            secondClicked = null;

            // فعال‌کردن دوباره آیکون‌ها
            AssignIconsToSquares();

            // تنظیم رنگ متن به همانند رنگ پس‌زمینه (برای مخفی کردن متن)
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.ForeColor = iconLabel.BackColor;
                }
            }
        }
    }
}
