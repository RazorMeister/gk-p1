using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt1
{
    public class Prompt : IDisposable
    {
        private Form prompt { get; set; }
        public string Result { get; }

        public Prompt(string text, string caption, string defaultResult)
        {
            Result = ShowDialog(text, caption, defaultResult);
        }

        private string ShowDialog(string text, string caption, string defaultResult)
        {
            prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };

            Label textLabel = new Label() { Left = 20, Top = 30, Text = text, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            TextBox textBox = new TextBox() { Left = 25, Top = 40, Width = 250, Text = defaultResult };
            Button confirmation = new Button() { Text = "Ok", Left = 200, Width = 75, Top = 75, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public void Dispose()
        {
            if (prompt != null)
                prompt.Dispose();
        }
    }
}
