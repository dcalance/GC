#include <GL/glut.h>
#include <math.h>

struct Point
{
	float x;
	float y;
};

void drawPoint(Point p)
{
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	glPushMatrix();
	glTranslatef(p.x, p.y, 0);

	glBegin(GL_POINTS);
	glColor3f(0.95f, 0.207, 0.031f);
	glVertex3f(0, 0, 0.0f);
	glEnd();
	glPopMatrix();
}

void drawTriangle(Point p1, Point p2, Point p3) 
{

	glBegin(GL_TRIANGLES);
	glVertex3f(p1.x, p1.y, 0.0);
	glColor3f(1.0f, 0.0f, 0.0f);
	glVertex3f(p2.x, p2.y, 0.0);
	glColor3f(0.0f, 1.0f, 0.0f);
	glVertex3f(p3.x, p3.y, 0.0f);
	glColor3f(0.0f, 0.0f, 1.0f);
	glEnd();
}



void drawLine(Point p1, Point p2, float width)
{
	float rotationAngle;
	float length;
	length = sqrtf(pow(p2.x - p1.x, 2) + pow(p1.y - p2.y, 2));
	float test = fabsf(p1.y - p2.y);
	rotationAngle = atan2(fabsf(p1.y - p2.y), fabsf(p2.x - p1.x)) * 180 / 3.14;
	if (p1.x < p2.x && p1.y > p2.y || p1.x > p2.x && p1.y < p2.y)
	{
		rotationAngle = -rotationAngle;
	}

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	glPushMatrix(); 
	glScalef(0.1f, 0.1f, 0.1f);
	glTranslatef((p1.x < p2.x)?p1.x : p2.x, (p1.x < p2.x) ? p1.y : p2.y, 0.0f);
	glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

	Point p3, p4;
	p1.x = 0;  p1.y = -width / 2;
	p2.x = length; p2.y = -width / 2;
	p3.x = length; p3.y = width / 2;
	p4.x = 0; p4.y = width / 2;
	drawTriangle(p2, p3, p1);
	drawTriangle(p4, p3, p1);
	glPopMatrix();
}


void draw()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glEnable(GL_POINT_SMOOTH);
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glPointSize(6.0);
	Point p1, p2;
	p1.x = -4; p1.y = 0;
	p2.x = 4; p2.y = 0;
	drawLine(p1, p2, 0.1f);
	p1.x = 4; p1.y = 0;
	p2.x = 0; p2.y = 4;
	drawLine(p1, p2, 0.1f);
	p1.x = 0; p1.y = 4;
	p2.x = -4; p2.y = 0;
	drawLine(p1, p2, 0.1f);
	p1.x = 0; p1.y = 0;
	drawPoint(p1);
	p1.x = 0.1; p1.y = 0;
	drawPoint(p1);
	p1.x = 0.2; p1.y = 0;
	drawPoint(p1);
	glutSwapBuffers();
}



int main(int argc, char **argv)
{
	// init GLUT and create Window
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(800, 640);
	glutCreateWindow("Lighthouse3D - GLUT Tutorial");

	// register callbacks
	glutDisplayFunc(draw);

	// enter GLUT event processing cycle
	glutMainLoop();

	return 1;
}