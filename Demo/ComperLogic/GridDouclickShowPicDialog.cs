using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ETCF
{
    public partial class GridDouclickShowPicDialog : Form
    {
        private string vehtype;
        private string getplateno;
        private string imagepath;
        private string forcetime;
        private string ovehtype;
        private string oplatenumber;
        private string vehlenth;
        private string vehheight;

        int Cartype = 0;


        FormDemo MF;

        public GridDouclickShowPicDialog(string veht, string plateno, string path, string forcetim, string otype, string onumber, string laser_vehlenth, string laser_vehheight, FormDemo fm)
        {
            InitializeComponent();

            vehtype = veht;//���⳵��
            getplateno = plateno;//���������
            imagepath = path;//ͼƬ·��
            forcetime = forcetim;//ʱ��
            ovehtype = otype;//Obu����
            oplatenumber = onumber;//OBU����
            vehlenth = laser_vehlenth;//����
            vehheight = laser_vehheight;//����
            MF = fm;
        }

        private void GridDouclickShowPicDialog_Load(object sender, EventArgs e)
        {
            int Shuttype=0;
            try
            {
                GlobalMember.MysqlInter.FindBlackOrWhiteCar(oplatenumber, ref Shuttype);
                if (Shuttype == -1 || Shuttype == -2)
                {
                    //�ָ�Ϊ��������
                    skinLabel1.Text = "��ǰ��������������  ";
                    //sbtnCheck.Text = "�ָ�Ϊ��������";
                }
                else if (Shuttype == 1)
                {
                    //�Ƴ�������
                    skinLabel1.Text = "��ǰ��������������  ";
                    //sbtnCheck.Text = "�Ƴ�������";
                }
                else if (Shuttype == 0)
                {
                    skinLabel1.Text = "��ǰ��������ͨ����  ";
                    //sbtnCheck.Text = "���������";
                }
                else
                {
                    skinLabel1.Text = "��ǰ������δ��������  ";
                    //sbtnCheck.Text = "������������";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {


                this.pictureBoxVeh.Load(imagepath);
                this.pictureBoxVeh.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ͼƬ����" + ex.ToString(), "��ʾ");
            }

            this.labelVeh.Text = "���⳵�ͣ�" + vehtype + "            OBU���ͣ�" + ovehtype + "\r\n��������ƺţ�" + getplateno + "   OBU���ƺţ�" + oplatenumber + " \r\nʱ��:" + forcetime + "\r\n";

        }

        private void wihiteList_Click(object sender, EventArgs e)
        {
            int res = 0;
            int Shuttype = 0;
            GlobalMember.MysqlInter.FindBlackOrWhiteCar(oplatenumber, ref Shuttype);

            if (Shuttype == 2)//����������
            {
                skinLabel1.Text = "��ǰ����������������";
            }
            else if (Shuttype == 1) //������
            {
                skinLabel1.Text = "��ǰ������������";
            }
            else if (Shuttype == -1)
            {
                skinLabel1.Text = "��ǰ��������ʱ������";
            }
            else if (Shuttype == -2)
            {
                skinLabel1.Text = "��ǰ������������";
            }
            if (Shuttype == 2)//����������
            {
                res = GlobalMember.MysqlInter.UpdateBlackOrWhiteCar(oplatenumber, 1);
               
            }
            else
            {
                res = GlobalMember.MysqlInter.UpdateBlackOrWhiteCar(oplatenumber, 1);
                
            }

            if (res != 0)
            {
                MessageBox.Show("����������ʧ��");
            }
            else
            {
                skinLabel1.Text = "��ǰ������������";
            }
        }

        private void blackList_Click(object sender, EventArgs e)
        {
            int res = 0;
            int Shuttype = 0;
            GlobalMember.MysqlInter.FindBlackOrWhiteCar(oplatenumber, ref Shuttype);

            if (Shuttype == 2)//����������
            {
                skinLabel1.Text = "��ǰ����������������";
            }
            else if (Shuttype == 1) //������
            {
                skinLabel1.Text = "��ǰ������������";
            }
            else if (Shuttype == -1)
            {
                skinLabel1.Text = "��ǰ��������ʱ������";
            }
            else if (Shuttype == -2)
            {
                skinLabel1.Text = "��ǰ������������";
            }

            if (Shuttype == 2)//����������
            {
                res = GlobalMember.MysqlInter.UpdateBlackOrWhiteCar(oplatenumber, -2);
                
            }
            else
            {
                res = GlobalMember.MysqlInter.UpdateBlackOrWhiteCar(oplatenumber, -2);
               
            }

            if (res != 0)
            {
                MessageBox.Show("����������ʧ��");
            }
            else
            {
                skinLabel1.Text = "��ǰ������������";
            }
        }

        private void pictureBoxVeh_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string PicPath = pictureBoxVeh.ImageLocation;
            Process.Start(PicPath);
        }
    }
}
