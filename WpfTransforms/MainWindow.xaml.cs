using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace WpfTransforms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        #region UIElement RenderTransform LayoutTransform

        double angleUIElement = 30;
        private void Grid_MouseDown_UIElement(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var rotateRender = new RotateTransform(angleUIElement);
            var rotateLayout = new RotateTransform(angleUIElement * -1);

            gridRenderTransform.RenderTransform = rotateRender;
            gridLayoutTransform.LayoutTransform = rotateLayout;

            angleUIElement += 30;

            //Title = "ah=" + borderLayout.ActualHeight.ToString() + ";aw=" + borderLayout.ActualWidth.ToString();
            //Title += ";renah=" + borderRender.ActualHeight.ToString() + ";renaw=" + borderRender.ActualWidth.ToString();
        }

        #endregion


        #region Трансформация текста

        private void Grid_MouseDown_TextEffects(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double centerX = textForEffect.ActualWidth / 2;
            double centerY = textForEffect.ActualHeight / 2;

            var transformGroup = new TransformGroup();

            var rotateTransform = new RotateTransform()
            {
                CenterX = centerX,
                CenterY = centerY
            };
            transformGroup.Children.Add(rotateTransform);

            var scaleTransform = new ScaleTransform()
            {
                CenterX = centerX,
                CenterY = centerY
            };
            transformGroup.Children.Add(scaleTransform);


            var textEffect = new TextEffect
            {
                Transform = transformGroup,
                PositionCount = textForEffect.Text.Length,
                PositionStart = 0
            };
            textForEffect.TextEffects.Add(textEffect);


            double time = 2;
            var daRotate = new DoubleAnimation
            {
                From = 0,
                To = 720,
                Duration = TimeSpan.FromSeconds(time)
            };
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, daRotate);

            double scaleFrom = 1;
            double scaleTo = 4;
            var daScaleX = new DoubleAnimation
            {
                From = scaleFrom,
                To = scaleTo,
                Duration = TimeSpan.FromSeconds(time / 2),
                AutoReverse = true
            };
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, daScaleX);

            var daScaleY = new DoubleAnimation
            {
                From = scaleFrom,
                To = scaleTo,
                Duration = TimeSpan.FromSeconds(time / 2),
                AutoReverse = true
            };
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, daScaleY);
        }

        #endregion


        #region Вращение градиентной кисти

        private void Grid_MouseDown_Brush(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var lgbrush = gridBrush.Background as LinearGradientBrush;

            var rotateBrush = new RotateTransform()
            {
                CenterX = 0.5,
                CenterY = 0.5
            };
            lgbrush.RelativeTransform = rotateBrush;


            var daAngle = new DoubleAnimation
            {
                From = 0,
                To = 720,
                Duration = TimeSpan.FromSeconds(3)
            };
            rotateBrush.BeginAnimation(RotateTransform.AngleProperty, daAngle);
        }


        #endregion


        #region Translate Border

        private void Grid_MouseDown_Translate(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double duration = 0.7;
            double begin = 0.3;
            int count = 0;
            AnimationBorder(border1, duration, begin * count++);
            AnimationBorder(border2, duration, begin * count++);
            AnimationBorder(border3, duration, begin * count++);
            AnimationBorder(border4, duration, begin * count++);
        }

        private void AnimationBorder(Border border, double duration, double begin)
        {
            border.RenderTransform = new TranslateTransform();

            var daTranslateX = new DoubleAnimation
            {
                From = 0,
                To = ((Grid)border.Parent).ActualWidth / 2 - border.Width / 2 - border.Margin.Left,
                Duration = TimeSpan.FromSeconds(duration),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(begin)
            };
            ((TranslateTransform)border.RenderTransform).BeginAnimation(TranslateTransform.XProperty, daTranslateX);

            var daTranslateY = new DoubleAnimation
            {
                From = border.Margin.Top,
                To = ((Grid)border.Parent).ActualHeight - border.Height,
                Duration = TimeSpan.FromSeconds(duration),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(begin)
            };
            ((TranslateTransform)border.RenderTransform).BeginAnimation(TranslateTransform.YProperty, daTranslateY);
        }

        #endregion


        #region Path Transform

        private void Grid_MouseDown_Path(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double time = 3;
            double rotatefrom = 0;
            double rotateto = 1080;

            double scalefrom = 1;
            double scaleto = 3;
            double translatefrom = pathRed.Margin.Left;
            double translateto = ((Grid)pathRed.Parent).ActualWidth - pathRed.ActualWidth;

            RotatePath(pathRed, translatefrom, translateto, rotatefrom, rotateto, scalefrom, scaleto, time);
            RotatePath(pathBlue, translatefrom, translateto, rotatefrom, rotateto * -1, scalefrom, scaleto, time);
        }


        private void RotatePath(Path path, double translatefrom, double translateto, double rotatefrom, double rotateto, double scalefrom, double scaleto, double time)
        {
            var daScaleX = new DoubleAnimation
            {
                From = scalefrom,
                To = scaleto,
                Duration = TimeSpan.FromSeconds(time),
                AutoReverse = true
            };

            var daScaleY = new DoubleAnimation
            {
                From = scalefrom,
                To = scaleto,
                Duration = TimeSpan.FromSeconds(time),
                AutoReverse = true
            };

            var daRotate = new DoubleAnimation
            {
                From = rotatefrom,
                To = rotateto,
                Duration = TimeSpan.FromSeconds(time),
                AutoReverse = true
            };

            var daTranslateX = new DoubleAnimation
            {
                From = translatefrom,
                To = translateto,
                Duration = TimeSpan.FromSeconds(time),
                AutoReverse = true
            };


            path.RenderTransformOrigin = new Point(0.5, 0.5);
            var rotate = new RotateTransform();
            var scale = new ScaleTransform();
            var translate = new TranslateTransform();
            var tg = new TransformGroup();
            tg.Children.Add(scale);
            tg.Children.Add(rotate);
            tg.Children.Add(translate);
            path.RenderTransform = tg;


            rotate.BeginAnimation(RotateTransform.AngleProperty, daRotate);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, daScaleY);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, daScaleX);
            translate.BeginAnimation(TranslateTransform.XProperty, daTranslateX);
        }


        #endregion

    }
}
