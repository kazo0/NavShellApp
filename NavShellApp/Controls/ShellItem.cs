using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavShellApp;
public partial class ShellItem : ContentControl
{
	#region Icon DependencyProperty
	public IconElement Icon
	{
		get { return (IconElement)GetValue(IconProperty); }
		set { SetValue(IconProperty, value); }
	}

	public static readonly DependencyProperty IconProperty =
		DependencyProperty.Register("Icon", typeof(IconElement), typeof(ShellItem), new PropertyMetadata(default));
	#endregion

	#region Title DependencyProperty
	public string Title
	{
		get { return (string)GetValue(TitleProperty); }
		set { SetValue(TitleProperty, value); }
	}

	public static readonly DependencyProperty TitleProperty =
		DependencyProperty.Register("Title", typeof(string), typeof(ShellItem), new PropertyMetadata(default));
	#endregion


}
