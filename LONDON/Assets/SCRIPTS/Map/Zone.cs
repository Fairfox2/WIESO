using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone
{


	// Implementing Mid-Point Circle
	// Drawing Algorithm
	public static void midPointCircleDraw(int x_centre, int y_centre, int r)
	{

		int x = r, y = 0;

		// Printing the initial point on the
		Map.Map_Rohstoffe[x + x_centre, y + y_centre] = 0;

		// When radius is zero only a single
		// point will be printed
		if (r > 0)
		{
			Map.Map_Rohstoffe[x + x_centre, -y + y_centre] = 0;
			Map.Map_Rohstoffe[y + x_centre, x + y_centre] = 0;
			Map.Map_Rohstoffe[-y + x_centre, x + y_centre] = 0;
		}

		// Initialising the value of P
		int P = 1 - r;
		while (x > y)
		{

			y++;

			// Mid-point is inside or on the perimeter
			if (P <= 0)
				P = P + 2 * y + 1;

			// Mid-point is outside the perimeter
			else
			{
				x--;
				P = P + 2 * y - 2 * x + 1;
			}

			// All the perimeter points have already
			// been printed
			if (x < y)
				break;

			// Printing the generated point and its
			// reflection in the other octants after
			// translation#
			Map.Map_Rohstoffe[x + x_centre, y + y_centre] = 0;
			Map.Map_Rohstoffe[-x + x_centre, y + y_centre] = 0;
			Map.Map_Rohstoffe[-x + x_centre, -y + y_centre] = 0;
			Map.Map_Rohstoffe[x + x_centre, -y + y_centre] = 0;

			// If the generated point is on the
			// line x = y then the perimeter points
			// have already been printed
			if (x != y)
			{
				Map.Map_Rohstoffe[y + x_centre, x + y_centre] = 0;
				Map.Map_Rohstoffe[-y + x_centre, x + y_centre] = 0;
				Map.Map_Rohstoffe[y + x_centre, -x + y_centre] = 0;
				Map.Map_Rohstoffe[-y + x_centre, -x + y_centre] = 0;
			}
		}

		// Driver code


		// This code is contributed by nitin mittal.
	}
}