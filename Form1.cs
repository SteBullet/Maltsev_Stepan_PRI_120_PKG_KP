using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Maltsev_Stepan_PRI_120_PKG_KP
{
    public partial class Form1 : Form
    {
        double angle = 3, angleX = -96, angleY = 0, angleZ = -30;
        double sizeX = 1, sizeY = 1, sizeZ = 1;

        double translateX = -9, translateY = -60, translateZ = -10;

        double cameraSpeed;
        float global_time = 0;

        bool isError = false;
        bool night = false;
        bool isNoseUsing = false;

        bool isSetChecked = false;
        bool isSetChecking = false;
        double deltaRotateHead, deltaNose;
        double deltaZStone;

        public int level;
        public int starter = 0;
        float laser_inc = 0;
        float laser_delta_inc = 0.5f;

        //Текстуры
        uint floorSign, teethSign, bodySign, eyeSign, targetSign;
        int imageId;
        string floorTexture = "floor.png";
        string teethTexture = "teeth.png";
        string bodyTexture = "body.png";
        string eyeTexture = "eye.png";
        string targetTexture = "target.png";

        //Взрыв с использованием системы частиц
        private readonly Explosion explosion = new Explosion(50, 120, 26, 40, 50);

        //Проигрывание аудио
        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация openGL (glut)
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // цвет очистки окна
            Gl.glClearColor(255, 255, 255, 1);

            // настройка порта просмотра
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(60, (float)AnT.Width / (float)AnT.Height, 0.1, 900);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 0;
            cameraSpeed = 5;
            label8.Visible = false;

            floorSign = genImage(floorTexture);
            bodySign = genImage(bodyTexture);
            teethSign = genImage(teethTexture);
            eyeSign = genImage(eyeTexture);
            targetSign = genImage(targetTexture);

            RenderTimer.Start();

            // Включение освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_NORMALIZE);
        }

        private void Draw()
        {

            // в зависимости от установленного режима отрисовываем сцену в черном или белом цвете
            if (comboBox3.SelectedIndex == 0)
            {
                Gl.glDisable(Gl.GL_LIGHTING);
                Gl.glClearColor(255, 255, 255, 1);
            }
            else
            {
                Gl.glEnable(Gl.GL_LIGHTING);
                Gl.glClearColor(0, 0, 0, 1);
            }

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            Gl.glRotated(angleX, 1, 0, 0);
            Gl.glRotated(angleY, 0, 1, 0);
            Gl.glRotated(angleZ, 0, 0, 1);
            Gl.glTranslated(translateX, translateY, translateZ);
            Gl.glScaled(sizeX, sizeY, sizeZ);

            explosion.Calculate(global_time);

            construct.drawFloor(floorSign);
            construct.drawRabbit(deltaRotateHead, deltaNose, eyeSign, teethSign);
            construct.drawBodySign(bodySign);
            construct.drawLaser(laser_inc);
            construct.drawTarget();
            construct.drawTargetSign(targetSign);
            drawFractal(level);

            if (deltaRotateHead > 10 && deltaRotateHead < 20)
            {
                Gl.glPushMatrix();
                explosion.SetNewPosition(50, 130, 90);
                explosion.SetNewPower(15);
                explosion.Boooom(global_time);
                Gl.glPopMatrix();
            }

            if (deltaNose < 42)
            {
                construct.drawNose(deltaNose, deltaZStone);
            }

            if (isNoseUsing)
            {
                if (deltaNose > 42)
                {
                    isNoseUsing = false;
                    Gl.glPushMatrix();
                    Gl.glPopMatrix();
                }
                else
                {
                    deltaNose += 3;
                    deltaZStone += 7;
                }
            }

            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            isSetChecked = false;
            isSetChecking = false;
            isNoseUsing = true;
            label8.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (deltaRotateHead == 0)
            {
                starter = 1;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            deltaRotateHead = 0;
            laser_delta_inc = 0.5f;
            laser_inc = 0;
            starter = 0;
            label8.Visible = false;
            label9.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                angle = 3; angleX = -90; angleY = 0; angleZ = -10;
                sizeX = 1; sizeY = 1; sizeZ = 1;
                translateX = -130; translateY = -10; translateZ = -55;
                label7.Visible = false;
                label9.Visible = false;
                button1.Visible = true;
                button2.Visible = false;
                WMP.controls.stop();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                translateX = -50; translateY = -30; translateZ = -120;
                angleX = -70;
                angleZ = 0;
                label7.Visible = true;
                label9.Visible = false;
                button1.Visible = true;
                button2.Visible = false;
                WMP.URL = @"robot.mp3";
                WMP.controls.play();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                //лево право перед зад верх низ
                translateX = -50; translateY = -100; translateZ = -115;
                angleX = -70;
                angleZ = 180;
                label7.Visible = false;
                label9.Visible = false;
                button1.Visible = false;
                button2.Visible = true;
                WMP.controls.stop();
            }
            AnT.Focus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnT.Focus();
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                translateY -= cameraSpeed;

            }
            if (e.KeyCode == Keys.S)
            {
                translateY += cameraSpeed;
            }
            if (e.KeyCode == Keys.A)
            {
                translateX += cameraSpeed;
            }
            if (e.KeyCode == Keys.D)
            {
                translateX -= cameraSpeed;

            }
            if (e.KeyCode == Keys.ControlKey)
            {
                translateZ += cameraSpeed;

            }
            if (e.KeyCode == Keys.Space)
            {
                translateZ -= cameraSpeed;
            }
            if (e.KeyCode == Keys.R)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX += angle;

                        break;
                    case 1:
                        angleY += angle;

                        break;
                    case 2:
                        angleZ += angle;

                        break;
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.E)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX -= angle;
                        break;
                    case 1:
                        angleY -= angle;
                        break;
                    case 2:
                        angleZ -= angle;
                        break;
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.D2)
            {
                if (deltaNose >= 42 && starter == 0)
                {
                    deltaRotateHead -= 2;
                }
                else
                {
                    if (starter == 1)
                    {
                        label8.Text = "После выстрела поворот невозможен";
                        label8.Visible = true;
                    }
                    else
                        label8.Visible = true;
                }

            }
            if (e.KeyCode == Keys.D3)
            {
                if (deltaNose >= 42 && starter == 0)
                {
                    deltaRotateHead += 2;
                }
                else
                {
                    if(starter == 1)
                    {
                        label8.Text = "После выстрела поворот невозможен";
                        label8.Visible = true;
                    }
                    else
                        label8.Visible = true;
                }

            }
            if (deltaRotateHead > 15)
            {
                isError = true;
                isSetChecking = true;
            }

        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            if (deltaRotateHead == 0)
            {
                button3.Visible = false;
            }
            else
            {
                button3.Visible = true;
            }
            laser_inc = (laser_inc + laser_delta_inc) * starter;
            if (laser_inc < 100)
                laser_delta_inc += 0.5f;
            else
            {
                label9.Visible = true;
                button3.Visible = true;
            }
            global_time += (float)RenderTimer.Interval / 1000;
            Draw();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            level = (int)numericUpDown1.Value;
            AnT.Focus();
        }

        Construct construct = new Construct();

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        public void drawFractal(int level)
        {
            Gl.glPushMatrix();

            Gl.glTranslated(55, 105, 82);
            Gl.glRotated(80, 0, 1, 0);
            Gl.glRotated(90, 0, 0, 1);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScalef(1, 0.65f, 0.5f);

            Gl.glBegin(Gl.GL_LINES);
            if (level >= 0 & level <= 3)
            {
                DrawKochLine(-10, -10, 20, 0, level);
            }
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        private const double SQRT_3 = 1.7320508075688772;

        public void DrawKochLine(double x1, double y1, double x2, double y2, int level)
        {
            if (level == 0)
            {
                // прямая линия
                Gl.glLineWidth(5f);
                Gl.glBegin(Gl.GL_LINES);
                Gl.glColor3f(0, 0, 0);
                Gl.glVertex2d(x1, y1);
                Gl.glVertex2d(x2, y2);
                Gl.glEnd();
            }
            else
            {
                //делим на 4 части
                double dx = (x2 - x1) / 3.0;
                double dy = (y2 - y1) / 3.0;

                // считаем координаты пиковых точек
                double peakX = x1 + dx - dy * Math.Cos(Math.PI / 3.0);
                double peakY = y1 + dy + dx * Math.Sin(Math.PI / 3.0);

                // рисовалка 4х сегментов
                DrawKochLine(x1, y1, x1 + dx, y1 + dy, level - 1);
                DrawKochLine(x1 + dx, y1 + dy, peakX, peakY, level - 1);
                DrawKochLine(peakX, peakY, x1 + 2 * dx, y1 + 2 * dy, level - 1);
                DrawKochLine(x1 + 2 * dx, y1 + 2 * dy, x2, y2, level - 1);
            }
        }


        private void информацияОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private uint genImage(string image)
        {
            uint sign = 0;
            Il.ilGenImages(1, out imageId);
            Il.ilBindImage(imageId);
            if (Il.ilLoadImage(image))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        sign = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        sign = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(1, ref imageId);
            return sign;
        }

        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            uint texObject;
            Gl.glGenTextures(1, out texObject);
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            switch (Format)
            {

                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

            }
            return texObject;
        }
    }
}
