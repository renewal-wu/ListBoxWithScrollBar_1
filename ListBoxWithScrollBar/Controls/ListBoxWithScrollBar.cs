using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;


namespace ListBoxWithScrollBar.Controls
{
    [TemplatePartAttribute(Name = "ItemNavigateSlider", Type = typeof(Slider))]
    public class ListBoxWithScrollBar : ListBox
    {
        Slider itemSlider;

        public ListBoxWithScrollBar()
        {
            DefaultStyleKey = typeof(ListBoxWithScrollBar);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            itemSlider = base.GetTemplateChild("ItemNavigateSlider") as Slider;
            if (itemSlider != null)
            {
                if (this.Items == null)
                {
                    itemSlider.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    itemSlider.Maximum = this.Items.Count - 1;
                    itemSlider.SmallChange = 1.0;
                    itemSlider.LargeChange = 10.0;
                    itemSlider.Value = itemSlider.Maximum;
                    itemSlider.ValueChanged += itemSlider_ValueChanged;
                }
            }
        }

        private void itemSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider targetSlider = sender as Slider;
            if (targetSlider != null)
            {
                Int32 scrollItemIndex = (Int32)(targetSlider.Maximum - targetSlider.Value);
                if (this.Items.Count >= scrollItemIndex)
                {
                    Object targetItem = this.Items.ElementAt(scrollItemIndex);
                    this.ScrollIntoView(targetItem);
                }
            }
        }
    }
}
