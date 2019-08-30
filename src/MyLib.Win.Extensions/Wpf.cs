using System.Collections.Generic;
using System.Windows.Controls;

namespace MyLib.Win.Extensions
{
	public static class Wpf
    {
		/// <summary>
		/// Load the enumerable data into the combobox with defualt values 
		/// description for DisplayMemberPath and id for SelectedValuePath 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="data"></param>
		public static void Fill(
			this ComboBox source
			, IEnumerable<object> data
			, long? selectedValue
		)
		{
			source.Fill(
				data
				, "Description"
				, "Id"
				, selectedValue
			);
		}

		/// <summary>
		/// Load the enumerable data into the combobox with the specified 
		/// displayMemberPath and selectedValuePath
		/// </summary>
		/// <param name="source"></param>
		/// <param name="data"></param>
		/// <param name="displayMemberPath"></param>
		/// <param name="selectedValuePath"></param>
		public static void Fill(
			this ComboBox source
			, IEnumerable<object> data
			, string displayMemberPath
			, string selectedValuePath
			, long? selectedValue
		)
		{
			source.ItemsSource = data;
			source.DisplayMemberPath = displayMemberPath;
			source.SelectedValuePath = selectedValuePath;

			if (selectedValue != null)
			{
				source.SelectedValue = selectedValue.Value;
			}
			
		}
    }
}
