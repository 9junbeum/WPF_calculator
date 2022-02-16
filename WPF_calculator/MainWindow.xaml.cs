using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_calculator
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        float result = 0.0f;      //결과값
        float num1 = 0.0f;        //저장된 숫자
        float num2 = 0.0f;        //새로 입력받은 숫자
        Boolean is_calc = false;
        Boolean is_oper_char = false;

        int[] index_of_oper = new int[100]; //연산자가 들어있는 index 포함하는 배열
        int index_count = 0;       //연산자 개수
        float[] num_arr = new float[100];   //실수의 문자열
        int num_count = 0;       //숫자 개수
        string[] oper_arr = new string[100];//연산자의 문자열
        int oper_count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void num_Btn_Click(object sender, RoutedEventArgs e)
        {
            Button numBtn = (Button)sender;

            is_oper_char = false;
            textBox1.AppendText(numBtn.Content.ToString());
        }

        private void oper_Btn_Click(object sender, RoutedEventArgs e)
        {
            Button operBtn = (Button)sender;

            if(operBtn.Content.ToString() == "-")
            {
                if(is_oper_char == true)
                {
                    textBox1.AppendText(operBtn.Content.ToString());
                    is_oper_char=true;
                    return;
                }
            }
            index_of_oper[index_count] = textBox1.Text.Length;
            textBox1.AppendText(operBtn.Content.ToString());
            index_count++;
            is_oper_char = true;
        }

        private void equal_Btn_Click(object sender, RoutedEventArgs e)
        {
            //임시로 저장하는 sub 
            string sub = "";
            oper_count = 0;
            num_count = 0;


            Button equaBtn = (Button)sender;
            //계산전 최종 문자열을 s에 저장한다.
            string s = textBox1.Text;

            //parsing
            for (int i = 0; i < s.Length; i++)
            {
                string c = s.Substring(i, 1);

                if (index_of_oper.Contains(i) && i != 0)
                {
                    //새로운 실수 모양의 문자열을 새로운 배열에 저장해야한다.
                    try
                    {
                        num_arr[num_count] = Convert.ToSingle(sub);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("잘못된 문자열 입니다.");
                        break;
                    }
                    sub = "";
                    num_count++;
                    //새로운 연산자도 배열에 저장해야한다.
                    oper_arr[oper_count] = s[i].ToString();
                    oper_count++;
                }
                else
                {
                    sub += c;
                }
            }
            //마지막 ^^
            try
            {
                num_arr[num_count] = Convert.ToSingle(sub);
            }
            catch (Exception)
            {
                MessageBox.Show("잘못된 문자열 입니다.");
            }

            //calculate
            for (int j = 0; j <= index_count; j++)
            {
                //아직 연산 안했으면
                if (is_calc == false)
                {
                    num1 = num_arr[j];
                    is_calc = true;
                }
                else
                {
                    num2 = num_arr[j];
                    switch (oper_arr[j - 1])
                    {
                        case "+":
                            num1 += num2;
                            break;

                        case "-":
                            num1 -= num2;
                            break;

                        case "*":
                            num1 *= num2;
                            break;

                        case "/":
                            num1 /= num2;
                            break;
                    }
                }
            }
            result = num1;
            num1 = 0;
            is_calc = false;
            textBox2.Clear();
            textBox2.AppendText(result.ToString());
        }

        private void erase_Btn_Click(object sender, RoutedEventArgs e)
        {
            string s = textBox1.Text;

            //지우려고 하는 것이 연산자이면, oper_count 한개 낮춰주고 해당 인덱스에 \0 넣음
            if (index_of_oper.Contains(textBox1.Text.Length - 1) && (textBox1.Text.Length != 1))
            {
                index_of_oper[--index_count] = '\0';
            }

            //textbox 관리
            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "";
            }
            textBox1.Text = s;
        }

        private void clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            //두개의 화면 모두 clear
            textBox1.Clear();
            textBox2.Clear();
            //매개변수 clear
            result = 0;
            num1 = 0;
            num2 = 0;
            //count도 초기화
            index_count = 0;       //연산자 개수
            oper_count = 0;
            num_count = 0;       //숫자 개수
                                 //배열도 다시 초기화

            index_of_oper = new int[100]; //연산자가 들어있는 index 포함하는 배열
            num_arr = new float[100];   //실수의 문자열
            oper_arr = new string[100];//연산자의 문자열
            is_calc = false;
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
