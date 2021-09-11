using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Media;
using System . Windows . Shapes;

using static WPFPages . Views . ThreeDeeBtnControl;

namespace WPFPages . Views
{
	class TestClass : ThreeDeeBtnControl
	{
		private Ellipse E1 = new Ellipse ( );

		public TestClass ( )
		{
			H3 . Fill = ( Brush ) TryFindResource ( "Green1" );
			E1 . Fill = ( Brush ) TryFindResource ( "Red1" );
		}
	}
}
