using System;
using System .Collections .Generic;
using System .Linq;
using System .Text;
using System .Threading .Tasks;
using System .Windows;
using System .Windows .Controls;
/// <summary>
/// 
/// OLDVERSION
/// </summary>
namespace WPFPages .CustomControls
{
	public class AnalogClock : Control
	{
		static  AnalogClock ( )
		{
			static void AnalogClock ( ) => DefaultStyleKeyProperty .OverrideMetadata ( typeof ( AnalogClock ) , new FrameworkPropertyMetadata ( typeof ( AnalogClock ) ) );
		}
	}
}
