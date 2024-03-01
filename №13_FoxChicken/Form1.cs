namespace _13_FoxChicken
{
    public partial class Form1 : Form
    {
        private Button foxField1 = new Button();
        private Button foxField2 = new Button();

        private int eatenChicks = 0;

        private readonly Button[,] buttons;

        private Button startButton = new Button { Text = "" };
        private Button endButton = new Button { Text = "" };

        private int rowFoxField1 = 2;
        private int columnFoxField1 = 2;
        private int rowFoxField2 = 2;
        private int columnFoxField2 = 4;

        private readonly Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            foxField1 = (Button)Controls["button9"];
            foxField2 = (Button)Controls["button11"];

            buttons = new Button[7, 7]
            {
                { null, null, button1, button2, button3, null, null },
                { null, null, button4, button5, button6, null, null },
                { button7, button8, button9, button10, button11, button12, button13 },
                { button14, button15, button16, button17, button18, button19, button20 },
                { button21, button22, button23, button24, button25, button26, button27 },
                { null, null, button28, button29, button30, null, null },
                { null, null, button31, button32, button33, null, null },
            };
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;

            if (senderButton.Text == "К")
            {
                startButton = senderButton;
                return;
            }

            if (senderButton.Text == "Л")
            {
                return;
            }

            if (startButton != null && senderButton.Text == "")
            {
                endButton = senderButton;

                if (IsButtonsClose())
                {
                    (startButton.Text, endButton.Text) = ("", "К");
                    startButton = endButton;
                    this.Refresh();

                    if (CheckVictoryCondition())
                    {
                        MessageBox.Show("Куры чемпионы!", "Вы одержали победу!", MessageBoxButtons.OK);
                        Application.Exit();
                    }

                    Thread.Sleep(300);

                    if (IsFoxEating())
                    {
                        if (eatenChicks >= 12)
                        {
                            MessageBox.Show("Лисы победили!", "Вы проиграли", MessageBoxButtons.OK);
                            Application.Exit();
                        }
                        return;
                    }

                    MoveFox();
                }
            }
        }

        private bool IsButtonsClose()
        {
            int startBtnTag = int.Parse(startButton.Tag.ToString());
            int endBtnTag = int.Parse(endButton.Tag.ToString());

            return (startBtnTag + 1 == endBtnTag) || (startBtnTag - 1 == endBtnTag) || (startBtnTag - 10 == endBtnTag);
        }

        private bool CheckVictoryCondition()
        {
            return button1.Text == "К" &&
                   button2.Text == "К" &&
                   button3.Text == "К" &&
                   button4.Text == "К" &&
                   button5.Text == "К" &&
                   button6.Text == "К" &&
                   button9.Text == "К" &&
                   button10.Text == "К" &&
                   button11.Text == "К";
        }

        private void MoveFox()
        {
            int fox = random.Next(1, 3);
            if (fox == 1)
            {
                CheckMove(ref rowFoxField1, ref columnFoxField1, ref foxField1);
            }
            else
            {
                CheckMove(ref rowFoxField2, ref columnFoxField2, ref foxField2);
            }
        }

        private void CheckMove(ref int rowFox, ref int columnFox, ref Button foxField)
        {
            Button endFoxField = new Button();
            int count = 20;
            while (count > 0)
            {
                count--;
                try
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            endFoxField = buttons[rowFox - 1, columnFox];
                            if (endFoxField != null && endFoxField.Text == "")
                            {
                                (foxField.Text, endFoxField.Text) = (endFoxField.Text, foxField.Text);
                                rowFox--;
                                break;
                            }
                            continue;

                        case 2:
                            endFoxField = buttons[rowFox + 1, columnFox];
                            if (endFoxField != null && endFoxField.Text == "")
                            {
                                (foxField.Text, endFoxField.Text) = (endFoxField.Text, foxField.Text);
                                rowFox++;
                                break;
                            }
                            continue;

                        case 3:
                            endFoxField = buttons[rowFox, columnFox - 1];
                            if (endFoxField != null && endFoxField.Text == "")
                            {
                                (foxField.Text, endFoxField.Text) = (endFoxField.Text, foxField.Text);
                                columnFox--;
                                break;
                            }
                            continue;

                        case 4:
                            endFoxField = buttons[rowFox, columnFox + 1];
                            if (endFoxField != null && endFoxField.Text == "")
                            {
                                (foxField.Text, endFoxField.Text) = (endFoxField.Text, foxField.Text);
                                columnFox++;
                                break;
                            }
                            continue;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }

                foxField = endFoxField;
                return;
            }
        }

        private bool IsFoxEating()
        {
            bool CanFoxEat(ref Button foxField, ref int rowFoxField, ref int columnFoxField)
            {
                Button leftPositionFox = columnFoxField - 1 >= 0 ? buttons[rowFoxField, columnFoxField - 1] ?? new Button() : null;
                Button upperPositionFox = rowFoxField - 1 >= 0 ? buttons[rowFoxField - 1, columnFoxField] ?? new Button() : null;
                Button lowerPositionFox = rowFoxField + 1 <= 6 ? buttons[rowFoxField + 1, columnFoxField] ?? new Button() : null;
                Button rightPositionFox = columnFoxField + 1 <= 6 ? buttons[rowFoxField, columnFoxField + 1] ?? new Button() : null;


                if (leftPositionFox != null && leftPositionFox.Text == "К")
                {
                    if (columnFoxField - 2 >= 0 && buttons[rowFoxField, columnFoxField - 2]?.Text == "")
                    {
                        (buttons[rowFoxField, columnFoxField - 2].Text, foxField.Text) = ("Л", "");
                        foxField = buttons[rowFoxField, columnFoxField - 2];
                        leftPositionFox.Text = "";
                        columnFoxField -= 2;
                        return true;
                    }
                }
                if (upperPositionFox != null && upperPositionFox.Text == "К")
                {
                    if (rowFoxField - 2 >= 0 && buttons[rowFoxField - 2, columnFoxField]?.Text == "")
                    {
                        (buttons[rowFoxField - 2, columnFoxField].Text, foxField.Text) = ("Л", "");
                        foxField = buttons[rowFoxField - 2, columnFoxField];
                        upperPositionFox.Text = "";
                        rowFoxField -= 2;
                        return true;
                    }
                }
                if (lowerPositionFox != null && lowerPositionFox.Text == "К")
                {
                    if (rowFoxField + 2 <= 6 && buttons[rowFoxField + 2, columnFoxField]?.Text == "")
                    {
                        (buttons[rowFoxField + 2, columnFoxField].Text, foxField.Text) = ("Л", "");
                        foxField = buttons[rowFoxField + 2, columnFoxField];
                        lowerPositionFox.Text = "";
                        rowFoxField += 2;
                        return true;
                    }
                }
                if (rightPositionFox != null && rightPositionFox.Text == "К")
                {
                    if (columnFoxField + 2 <= 6 && buttons[rowFoxField, columnFoxField + 2]?.Text == "")
                    {
                        (buttons[rowFoxField, columnFoxField + 2].Text, foxField.Text) = ("Л", "");
                        foxField = buttons[rowFoxField, columnFoxField + 2];
                        rightPositionFox.Text = "";
                        columnFoxField += 2;
                        return true;
                    }
                }
                return false;
            }

            int count = 0;
            while (CanFoxEat(ref foxField1, ref rowFoxField1, ref columnFoxField1))
            {
                count++;
                eatenChicks++;
                this.Refresh();
                Thread.Sleep(500);
            }
            if (count > 0)
                return true;
            while (CanFoxEat(ref foxField2, ref rowFoxField2, ref columnFoxField2))
            {
                count++;
                eatenChicks++;
                this.Refresh();
                Thread.Sleep(500);
            }
            return count > 0;
        }
    }
}
