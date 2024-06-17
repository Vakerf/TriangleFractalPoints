namespace FractalPoints;

public static class PointHelper
{
    /// <summary>
    /// Return one of the three point given in parameters chosen randomly.
    /// </summary>
    /// <param name="pt1">First of the three points to choose from.</param>
    /// <param name="pt2">Second of the three point to choose from.</param>
    /// <param name="pt3">Third of the three point to choose from.</param>
    /// <returns>One of the three point chosen randomly.</returns>
    public static Point SelectRandomStartPoint(Point pt1, Point pt2, Point pt3)
    {
        var random = new Random();
        int result = random.Next(3);

        switch (result)
        {
            case 0:
                return pt1;
            case 1:
                return pt2;
            case 2:
                return pt3;
            default:
                return pt1;
        }
    }
    
    /// <summary>
    /// Select a point randomly chosen inside a triangle formed by the three point given in parameters.
    /// Also, the returned point doesn't share coordinates with any point present in the given list of points.
    /// </summary>
    /// <param name="alreadyCreatedPoints">The list of reference points not to share coordinates with.</param>
    /// <param name="pt1">The first point of the triangle.</param>
    /// <param name="pt2">The second point of the triangle.</param>
    /// <param name="pt3">the third point of the triangle.</param>
    /// <returns>A new <see cref="Point"/>.</returns>
    public static Point SelectRandomPointWithinTriangle(List<Point> alreadyCreatedPoints, Point pt1, Point pt2, Point pt3)
    {
        Random random = new Random();
        Point randomPointInTriangle;
        do
        {
            var x = random.Next(100, 600);
            var y = random.Next(100, 300);
            randomPointInTriangle = new Point(x, y);
        } while (!IsPointInTriangle(randomPointInTriangle, pt1, pt2, pt3) &&
                 !alreadyCreatedPoints.Any(_ => _.X == randomPointInTriangle.X && _.Y == randomPointInTriangle.Y));

        return randomPointInTriangle;
    }
    
    /// <summary>
    /// Return a new point that is in the middle of the line between the two given points.
    /// </summary>
    /// <param name="pt1">The first point of the line.</param>
    /// <param name="pt2">The second point of the line.</param>
    /// <returns>A new <see cref="Point"/>.</returns>
    public static Point GetPointBetweenTwoPoints(Point pt1, Point pt2)
    {
        int newX;
        int newY;
        
        // calculate X.
        if (pt1.X > pt2.X)
        {
            var distBetweenPoints = pt1.X - pt2.X;
            newX = pt1.X - (distBetweenPoints / 2);
        }
        else if (pt1.X < pt2.X)
        {
            var distBetweenPoints = pt2.X - pt1.X;
            newX = pt2.X - (distBetweenPoints / 2);
        }
        else
        {
            newX = pt1.X;
        }

        // calculate Y.
        if (pt1.Y > pt2.Y)
        {
            var distBetweenPoints = pt1.Y - pt2.Y;
            newY = pt1.Y - (distBetweenPoints / 2);
        }
        else if (pt1.Y < pt2.Y)
        {
            var distBetweenPoints = pt2.Y - pt1.Y;
            newY = pt2.Y - (distBetweenPoints / 2);
        }
        else
        {
            newY = pt1.Y;
        }

        return new Point(newX, newY);
    }
    
    private static float Sign(Point p1, Point p2, Point p3)
    {
        return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
    }

    private static bool IsPointInTriangle(Point pointToTest, Point pt1, Point pt2, Point pt3)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = Sign(pointToTest, pt1, pt2);
        d2 = Sign(pointToTest, pt2, pt3);
        d3 = Sign(pointToTest, pt3, pt1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }

}