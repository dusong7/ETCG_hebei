using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ControlExs;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
namespace ETCF
{
    public partial class frmLogin : Form
    {
        public FormDemo fm;
        public frmLogin()
        {
            InitializeComponent();
        }
        //SqlConnection conn = BaseClass.DBCon();

        private void frmLogin_Load(object sender, EventArgs e)
        {
            cbbType.SelectedIndex = 0;
        }
        //��ȡ������Ϣ
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (cbbType.Text.Trim() == "��ѡ���¼���")
            {
                QQMessageBox.Show(
                               this,
                               "��ʾ����ѡ���¼��ݣ�",
                               "����",
                               QQMessageBoxIcon.Warning,
                               QQMessageBoxButtons.OK);
            }
            else if (cbbType.Text.Trim() == "�ο͵�¼")
            {
                GlobalMember.isLogin = true;
                QQMessageBox.Show(
                               this,
                               "��ʾ��Ŀǰ��½��ʽΪ�οͣ����ֹ��ܿ������ޣ�",
                               "֪ͨ",
                               QQMessageBoxIcon.OK,
                               QQMessageBoxButtons.OK);
            
            }
            else
            {
                if (txtUser.Text.Trim() == "" || txtPwd.Text.Trim() == "")
                {
                    QQMessageBox.Show(
                               this,
                               "��ʾ���������¼�û��������룡",
                               "����",
                               QQMessageBoxIcon.Warning,
                               QQMessageBoxButtons.OK);
                }
                else
                {
                    
                    StringBuilder temp = new StringBuilder();
                    GetPrivateProfileString("SQLServer", "sql_type", "0", temp, 255, AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
                    GlobalMember.SqlType = temp.ToString().Equals("SQLServer") ? "SQLServer" : "MySql";
                    GetPrivateProfileString("SQLServer", "sql_ip", "0", temp, 255, AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
                    string sql_ip = temp.ToString();
                    GetPrivateProfileString("SQLServer", "sql_dbname", "0", temp, 255, AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
                    string sql_dbname = temp.ToString();
                    GetPrivateProfileString("SQLServer", "sql_username", "0", temp, 255, AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
                    string sql_username = temp.ToString();
                    GetPrivateProfileString("SQLServer", "sql_password", "0", temp, 255, AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
                    string sql_password = temp.ToString();
                    GetPrivateProfileString("SQLServer", "sql_port", "0", temp, 255, AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
                    string sql_port = temp.ToString();
                    string txtSql = "Select * From SoftUser";
                    //�������ݿ�����
                    if (GlobalMember.SqlType.Equals("SQLServer"))
                    {
                        string myConStr = @"Persist Security Info=True;User ID=" + sql_username + ";Password =" + sql_password + ";Initial Catalog=" + sql_dbname + ";Data Source=" + sql_ip + "; MultipleActiveResultSets = True";
                        SqlConnection myCon = new SqlConnection();
                        myCon.ConnectionString = myConStr;
                        //����sqlcommand

                        SqlCommand myCom = new SqlCommand(txtSql, myCon);
                        txtSql = "select * from SoftUser where User_name ='" + txtUser.Text + " '";
                        try
                        {
                            myCon.Open();
                            SqlDataReader myRD = myCom.ExecuteReader();
                            while (myRD.Read())
                            {
                                if (myRD[1].ToString().Trim() == txtUser.Text && myRD[2].ToString().Trim() == MD5encrypt(txtPwd.Text))
                                {
                                    QQMessageBox.Show(
                                    this,
                                    "��ʾ����½�ɹ���",
                                    "��½�ɹ�",
                                    QQMessageBoxIcon.OK,
                                    QQMessageBoxButtons.OK);
                                    GlobalMember.isLogin = true;
                                    this.Close();

                                }
                            }
                            if (!GlobalMember.isLogin)
                                QQMessageBox.Show(
                                    this,
                                    "��ʾ������Ա�û������������",
                                    "����",
                                    QQMessageBoxIcon.Error,
                                    QQMessageBoxButtons.OK);

                        }
                        catch (SqlException ex)
                        {
                            QQMessageBox.Show(
                                    this,
                                    ex.ToString(),
                                    "����",
                                    QQMessageBoxIcon.Error,
                                    QQMessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        string myConStr = @"Persist Security Info=True;UserId=" + sql_username + ";Password =" + sql_password + ";Database=" + sql_dbname + ";DataSource=" + sql_ip + ";CharSet=utf8;port=" + sql_port;
                        MySqlConnection myCon = new MySqlConnection();
                        myCon.ConnectionString = myConStr;
                        //����sqlcommand

                        MySqlCommand myCom = new MySqlCommand(txtSql, myCon);
                        txtSql = "select * from SoftUser where User_name ='" + txtUser.Text + " '";
                        try
                        {
                            myCon.Open();
                            MySqlDataReader myRD = myCom.ExecuteReader();
                            while (myRD.Read())
                            {
                                if (myRD[1].ToString().Trim() == txtUser.Text && myRD[2].ToString().Trim() == MD5encrypt(txtPwd.Text))
                                {
                                    QQMessageBox.Show(
                                    this,
                                    "��ʾ����½�ɹ���",
                                    "��½�ɹ�",
                                    QQMessageBoxIcon.OK,
                                    QQMessageBoxButtons.OK);
                                    GlobalMember.isLogin = true;
                                    this.Close();

                                }
                            }
                            if (!GlobalMember.isLogin)
                                QQMessageBox.Show(
                                    this,
                                    "��ʾ������Ա�û������������",
                                    "����",
                                    QQMessageBoxIcon.Error,
                                    QQMessageBoxButtons.OK);

                        }
                        catch (SqlException ex)
                        {
                            QQMessageBox.Show(
                                    this,
                                    ex.ToString(),
                                    "����",
                                    QQMessageBoxIcon.Error,
                                    QQMessageBoxButtons.OK);
                        }
                    
                    }
                }

            }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPwd.Text = "";
            txtUser.Text = "";
        }
        //MD5����
        public static string MD5encrypt(string encryptString)
        {
            byte[] result = Encoding.Default.GetBytes(encryptString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string encryptResult = BitConverter.ToString(output).Replace("-", "");
            return encryptResult;
        }

    }
}