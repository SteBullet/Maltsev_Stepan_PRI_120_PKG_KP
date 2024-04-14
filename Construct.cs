using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Maltsev_Stepan_PRI_120_PKG_KP
{
    //Класс RGB для удобства задания цвета
    class RGB
    {
        private float R;
        private float G;
        private float B;

        public RGB(float R, float G, float B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public float getR()
        {
            return R;
        }

        public float getG()
        {
            return G;
        }

        public float getB()
        {
            return B;
        }
    }


    class Construct
    {
        float deltaColor = 0;
        public float goods_move = 0;

        private void setColor(float R, float G, float B)
        {
            RGB color = new RGB(R - deltaColor, G - deltaColor, B - deltaColor);
            Gl.glColor3f(color.getR(), color.getG(), color.getB());
        }

        public void drawRabbit(double deltaX, double deltaNose, uint eyesign, uint sign)
        {
            Gl.glPushMatrix();

            Gl.glTranslated(50, 130, 60);
            Gl.glPushMatrix();
            Gl.glScaled(1.2, 1, 1.5);
            setColor(0.7f, 0.8f, 0.5f);
            Glut.glutSolidCube(40);
            Gl.glPopMatrix();

            Gl.glTranslated(-50, 0, 10);
            Gl.glPushMatrix();
            Gl.glScaled(5, 1, 1);
            Gl.glRotated(90, 0, 1, 0);
            setColor(0.3f, 0.3f, 0.3f);
            Glut.glutSolidCylinder(5, 20, 15, 2);
            Gl.glPopMatrix();

            Gl.glTranslated(40, 0, -70);
            Gl.glPushMatrix();
            Gl.glScaled(0.7, 0.7, 2);
            setColor(0.3f, 0.3f, 0.3f);
            Glut.glutSolidCylinder(10, 20, 15, 2);
            Gl.glPopMatrix();

            Gl.glTranslated(20, 0, 0);
            Gl.glPushMatrix();
            Gl.glScaled(0.7, 0.7, 2);
            setColor(0.3f, 0.3f, 0.3f);
            Glut.glutSolidCylinder(10, 20, 15, 2);
            Gl.glPopMatrix();

            Gl.glTranslated(-10, 0 , 102);
            Gl.glRotated(0 + deltaX, 0, 0, 1);
            Gl.glPushMatrix();
            Gl.glScaled(1.2, 1, 0.6);
            setColor(0.3f, 0.3f, 0.3f);
            Glut.glutSolidCube(40);
            Gl.glPopMatrix();

            Gl.glTranslated(0, -30, -7.5);
            Gl.glPushMatrix();
            Gl.glScaled(1.2, 0.5, 0.2);
            setColor(0.3f, 0.3f, 0.3f);
            Glut.glutSolidCube(40);
            Gl.glPopMatrix();

            //ears
            Gl.glTranslated(-10, 20, 20);
            Gl.glPushMatrix();
            if (deltaNose >= 42)
            {
                float[] position = { 0, 0, 30, 1 };
                float[] direction = { 0, 1, 0 };
                float[] ambient = { 0.13f, 0.94f, 0.93f, 1 };

                //СВЕЧЕНИЕ
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, position);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, direction);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, ambient);
            }
            else
            {
                float[] position = { 0, 0, 0, 1 };
                float[] direction = { 0, 1, 0 };
                float[] ambient = { 0, 0, 0, 1 };

                //СВЕЧЕНИЕ
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, position);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, direction);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, ambient);
            }
            Gl.glScaled(0.5, 0.5, 1);
            setColor(0.9f, 1f, 0.1f);
            Glut.glutSolidCylinder(10, 20, 15, 2);
            Gl.glColor3f(0.5f, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCylinder(10, 20, 15, 2);
            Gl.glPopMatrix();

            Gl.glTranslated(20, 0, 0);
            Gl.glPushMatrix();
            Gl.glScaled(0.5, 0.5, 1);
            setColor(0.9f, 1f, 0.1f);
            Glut.glutSolidCylinder(10, 20, 15, 2);
            Gl.glColor3f(0.5f, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCylinder(10, 20, 15, 2);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-35, -26, -24);
            Gl.glScaled(2.4, 1, 1.1);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, sign);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 20);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(10, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(10, 5, 20);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-35, -7, -15);
            Gl.glScaled(2.4, 1, 1.6);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, eyesign);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 20);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(10, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(10, 5, 20);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            if(deltaNose >= 42)
            {
                Gl.glPushMatrix();
                Gl.glRotated(90, 0, 0, 1);
                Gl.glTranslated(-27, 9, -13);
                setColor(0.08f, 0.08f, 0.08f);
                Gl.glScaled(1, 1, 1);
                Glut.glutSolidSphere(3, 12, 12);
                Gl.glPopMatrix();
            }

            Gl.glPopMatrix();
        }

        //Отрисовка пола
        public void drawFloor(uint sign)
        {
            Gl.glPushMatrix();

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, sign);

            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(-250, 0, 0);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(250, 0, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(250, 300, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(-250, 300, 0);
            Gl.glTexCoord2f(1, 0);

            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glPopMatrix();

        }

        //Отрисовка тела
        public void drawBodySign(uint sign)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(25, 112, 30);
            Gl.glScaled(2.6,1,6);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, sign);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 20);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(10, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(10, 5, 20);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }

        //Отрисовка носа
        public void drawNose(double x, double z)
        {
            Gl.glPushMatrix();
            Gl.glRotated(90, 0, 0, 1);
            Gl.glTranslated(105, x - 93, z + 1);
            setColor(0.08f, 0.08f, 0.08f);
            Gl.glScaled(1, 1, 1);
            Glut.glutSolidSphere(3, 12, 12);
            Gl.glPopMatrix();
        }

        //Отрисовка лазеров
        public void drawLaser(double move)
        {
            Gl.glPushMatrix();

            if (move < 145)
            {
                Gl.glPushMatrix();
                Gl.glRotated(90, 0, 0, 1);
                Gl.glTranslated(130, -37, 105);
                Gl.glRotated(-90, 1, 0, 0);
                Gl.glRotated(-90, 0, 1, 0);
                setColor(0.8f, 0.4f, 0.3f);
                Gl.glScaled(1, 1, 1);
                Glut.glutSolidCylinder(2, 0 + move, 12, 2);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glRotated(90, 0, 0, 1);
                Gl.glTranslated(130, -60, 105);
                Gl.glRotated(-90, 1, 0, 0);
                Gl.glRotated(-90, 0, 1, 0);
                setColor(0.8f, 0.4f, 0.3f);
                Gl.glScaled(1, 1, 1);
                Glut.glutSolidCylinder(2, 0 + move, 12, 2);
                Gl.glPopMatrix();
            }
            else
            {
                Gl.glPushMatrix();
                Gl.glRotated(90, 0, 0, 1);
                Gl.glTranslated(130, -37, 105);
                Gl.glRotated(-90, 1, 0, 0);
                Gl.glRotated(-90, 0, 1, 0);
                setColor(0.8f, 0.4f, 0.3f);
                Gl.glScaled(1, 1, 1);
                Glut.glutSolidCylinder(2, 145, 12, 2);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glRotated(90, 0, 0, 1);
                Gl.glTranslated(130, -60, 105);
                Gl.glRotated(-90, 1, 0, 0);
                Gl.glRotated(-90, 0, 1, 0);
                setColor(0.8f, 0.4f, 0.3f);
                Gl.glScaled(1, 1, 1);
                Glut.glutSolidCylinder(2, 145, 12, 2);
                Gl.glPopMatrix();
            }

            Gl.glPopMatrix();
        }

        //Отрисовка мишени
        public void drawTarget()
        {
            Gl.glPushMatrix();

            Gl.glPushMatrix();
            Gl.glRotated(90, 0, 0, 1);
            Gl.glTranslated(-10, -47, 95);
            Gl.glRotated(-90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            setColor(0.3f, 0.4f, 0.3f);
            Gl.glScaled(1, 1, 1);
            Glut.glutSolidCylinder(35, 5, 25, 2);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка мишени
        public void drawTargetSign(uint sign)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(75, -13, 65);
            Gl.glScaled(3, 1, 6);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glRotated(180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, sign);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 20);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(10, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(10, 5, 20);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }

    }
}

