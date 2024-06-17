using System.ComponentModel;

namespace FractalPoints;
using System.Drawing;

public partial class Form1 : Form
{
    private readonly Point _upPoint = new Point(550, 100);
    private readonly Point _leftPoint = new Point(100, 700);
    private readonly Point _rightPoint = new Point(1000, 700);
    private readonly List<Point> _createdPoints = new List<Point>();
    private Graphics _graphics;
    private readonly Pen _pen = new Pen(Color.Black); 

    public Form1()
    {
        InitializeComponent();
    }

    private void form1_Paint(object sender, PaintEventArgs e)
    {
        _graphics = e.Graphics;

        DrawPoint(_upPoint, _pen, _graphics);
        DrawPoint(_leftPoint, _pen, _graphics);
        DrawPoint(_rightPoint, _pen, _graphics);
        
        var basePoint = PointHelper.SelectRandomPointWithinTriangle(_createdPoints, _upPoint, _leftPoint, _rightPoint);
        
        while (_createdPoints.Count <= 100000)
        {
            var startPoint = PointHelper.SelectRandomStartPoint(_upPoint, _leftPoint, _rightPoint);
            var newPoint = PointHelper.GetPointBetweenTwoPoints(startPoint, basePoint);
            basePoint = newPoint;
            
            _createdPoints.Add(newPoint);
            DrawPoint(newPoint, _pen, _graphics); 
        }
    }

    private void DrawPoint(Point point, Pen pen, Graphics graphics)
    {
        // Since System.Drawings doesn't allow the drawing of a point, we have to draw a tiny tiny line between two adjacent points.
        var secondPoint = new Point(point.X + 1, point.Y);
        graphics.DrawLine(pen, point, secondPoint);
    }
}