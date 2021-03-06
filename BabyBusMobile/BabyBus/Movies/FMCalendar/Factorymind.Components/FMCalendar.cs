﻿//	New implementations, refactoring and restyling - FactoryMind || http://factorymind.com 
//  Converted to MonoTouch on 1/22/09 - Eduardo Scoz || http://escoz.com
//  Originally reated by Devin Ross on 7/28/09  - tapku.com || http://github.com/devinross/tapkulibrary
//
/*
 
 Permission is hereby granted, free of charge, to any person
 obtaining a copy of this software and associated documentation
 files (the "Software"), to deal in the Software without
 restriction, including without limitation the rights to use,
 copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the
 Software is furnished to do so, subject to the following
 conditions:
 
 The above copyright notice and this permission notice shall be
 included in all copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 OTHER DEALINGS IN THE SOFTWARE.
 
 */

using System;
using System.Globalization;
using CoreGraphics;
using Foundation;
using UIKit;

namespace BabyBus.iOS
{

    public delegate void DateSelected(DateTime date);
    public delegate void MonthChanged(DateTime monthSelected);

    public class FMCalendar : UIView
    {
        /// <summary>
        /// Fired when new date selected.
        /// </summary>
        public Action<DateTime> DateSelected;

        /// <summary>
        /// Fired when date selection finished
        /// </summary>
        public Action<DateTime> DateSelectionFinished;

        /// <summary>
        /// Fired when Selected month changed
        /// </summary>
        public Action<DateTime> MonthChanged;

        /// <summary>
        /// Mark with a dot dates that fulfill the predicate
        /// </summary>
        public Func<DateTime, bool> IsDayMarkedDelegate;

        /// <summary>
        /// Turn gray dates that fulfill the predicate
        /// </summary>
        public Func<DateTime, bool> IsDateAvailable;

        /// <summary>
        /// Gets the current selected Date.
        /// </summary>
        /// <value>The current selected date.</value>
        public DateTime CurrentSelectedDate { get { return SelectedDate; } }

        public DateTime CurrentMonthYear;

        protected DateTime CurrentDate { get; set; }

        internal DateTime SelectedDate { get; set; }

        private UIScrollView _scrollView;
        private bool calendarIsLoaded;

        private MonthGridView _monthGridView;
        private UIButton _leftButton, _rightButton;

        // User Customizations

        /// <summary>
        /// If true, Sunday will be showed as the first day of the week, otherwise the first one will be Monday
        /// </summary>
        /// <value><c>true</c> if sunday first; otherwise, <c>false</c>.</value>
        public Boolean SundayFirst { get; set; }

        /// <summary>
        /// Format string used to display the month's name
        /// </summary>
        /// <value>The month format string.</value>
        public String MonthFormatString { get; set; }

        /// <summary>
        /// Specify the color for the selected date
        /// </summary>
        /// <value>The color of the selection.</value>
        public UIColor SelectionColor { get; set; }

        /// <summary>
        /// Specify the color for the today circle
        /// </summary>
        /// <value>The color of the selection.</value>
        public UIColor TodayCircleColor { get; set; }

        public UIColor TodayMarkColor { get; set; }

        /// <summary>
        /// Specify the background color for the calendar
        /// </summary>
        /// <value>The color of the selection.</value>
        public UIColor MonthBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the left arrow image.
        /// </summary>
        /// <value>The left arrow.</value>
        public UIImage LeftArrow { get; set; }

        /// <summary>
        /// Gets or sets the right arrow image.
        /// </summary>
        /// <value>The right arrow.</value>
        public UIImage RightArrow { get; set; }

        /// <summary>
        /// Gets or sets the top bar View.
        /// </summary>
        /// <value>The top bar.</value>
        public UIImage TopBar { get; set; }

        static CGRect _headerViewSize = new  CGRect(0, 0, 280, 60);
        static CGRect _mainViewSize = new  CGRect(0, 0, 280, 300);

        public static CoreGraphics.CGRect HeaderViewSize { get { return _headerViewSize; } }

        public static CoreGraphics.CGRect MainViewSize { get { return _mainViewSize; } }

        private float HeaderMiddleY { get { return (float)(HeaderViewSize.Height / 2) - (HeaderElementSize / 2); } }

        private float HeaderElementSize { get { return (float)(HeaderViewSize.Height - (HeaderViewSize.Height * 0.50f)); } }

        private float HeaderBorderOffset { get { return 10f; } }

        private float DayNameHeight { get { return 10f; } }

        internal int DayCellWidth { get { return 40; } }

        internal int DayCellHeight { get { return 40; } }


        public FMCalendar()
            : base(MainViewSize)
        {
            Initialize();
        }

       

        private void Initialize()
        {
            CurrentDate = DateTime.Now.Date;
            SelectedDate = CurrentDate;
            CurrentMonthYear = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);

            // Defaults

            SundayFirst = false;

            MonthFormatString = "MMMM yyyy";

            MonthBackgroundColor = UIColor.White;
            SelectionColor = UIColor.Red;
            TodayCircleColor = UIColor.Green;
            TodayMarkColor = UIColor.Red;

            LeftArrow = UIImage.FromFile("leftArrow.png");
            RightArrow = UIImage.FromFile("rightArrow.png");
        }

        public override bool CanBecomeFirstResponder
        {
            get
            {
                return true;
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
        }

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            return base.HitTest(point, uievent);
        }

        public override void SetNeedsDisplay()
        {
            base.SetNeedsDisplay();

//            AdjustBackgroundColor();

            if (_monthGridView != null)
                _monthGridView.Update();
        }

        public override void LayoutSubviews()
        {
            AdjustBackgroundColor();

            if (calendarIsLoaded)
                return;

            _scrollView = new UIScrollView(new CoreGraphics.CGRect(0, HeaderViewSize.Height, MainViewSize.Width, MainViewSize.Height - HeaderViewSize.Height))
            {
                ContentSize = new CoreGraphics.CGSize(MainViewSize.Width, MainViewSize.Height),
                ScrollEnabled = false,
                Frame = new CoreGraphics.CGRect(0, HeaderViewSize.Height, MainViewSize.Width, MainViewSize.Height),
                BackgroundColor = MonthBackgroundColor,
            };

            LoadButtons();

            LoadInitialGrids();

            AddSubview(_scrollView);
            _scrollView.AddSubview(_monthGridView);

            calendarIsLoaded = true;
        }

        public void DeselectDate()
        {
            if (_monthGridView != null)
                _monthGridView.DeselectDayView();
        }

        private void LoadButtons()
        {
            _leftButton = UIButton.FromType(UIButtonType.Custom);
            _leftButton.TouchUpInside += HandlePreviousMonthTouch;
            _leftButton.ContentMode = UIViewContentMode.ScaleAspectFit;
            _leftButton.ContentEdgeInsets = new UIEdgeInsets(10, 5, 5, 5);
            _leftButton.SetImage(LeftArrow, UIControlState.Normal);
            AddSubview(_leftButton);


            _leftButton.Frame = new CoreGraphics.CGRect(HeaderBorderOffset, HeaderMiddleY, HeaderElementSize, HeaderElementSize);

           

            _rightButton = UIButton.FromType(UIButtonType.Custom);
            _rightButton.TouchUpInside += HandleNextMonthTouch;
            _rightButton.ContentMode = UIViewContentMode.ScaleAspectFit;
            _rightButton.ContentEdgeInsets = new UIEdgeInsets(10, 5, 5, 5);
            _rightButton.SetImage(RightArrow, UIControlState.Normal);
            AddSubview(_rightButton);


            _rightButton.Frame = new CoreGraphics.CGRect(HeaderViewSize.Width - HeaderElementSize - HeaderBorderOffset, HeaderMiddleY, HeaderElementSize, HeaderElementSize);

           
        }

        private void HandlePreviousMonthTouch(object sender, EventArgs e)
        {
            MoveCalendarMonths(false, true);
        }

        private void HandleNextMonthTouch(object sender, EventArgs e)
        {
            MoveCalendarMonths(true, true);
        }

        /// <summary>
        /// Moves the calendar months.
        /// </summary>
        /// <param name="upwards">If set to <c>true</c> moves the month upwards.</param>
        /// <param name="animated">If set to <c>true</c> the transition will be animated.</param>
        public void MoveCalendarMonths(bool upwards, bool animated)
        {
            if (_monthGridView == null)
                return;

            CurrentMonthYear = CurrentMonthYear.AddMonths(upwards ? 1 : -1);
            UserInteractionEnabled = false;

            // Dispatch event
            if (MonthChanged != null)
                MonthChanged(CurrentMonthYear);

            var gridToMove = CreateNewGrid(CurrentMonthYear);
            var pointsToMove = (upwards ? 0 + _monthGridView.Lines : 0 - _monthGridView.Lines) * DayCellHeight;

            if (upwards && gridToMove.weekdayOfFirst == 0)
                pointsToMove += DayCellHeight;
            if (!upwards && _monthGridView.weekdayOfFirst == 0)
                pointsToMove -= DayCellHeight;


            gridToMove.Frame = new CoreGraphics.CGRect(new CoreGraphics.CGPoint(0, pointsToMove), gridToMove.Frame.Size);


            _scrollView.AddSubview(gridToMove);

            if (animated)
            {
                UIView.BeginAnimations("changeMonth");
                UIView.SetAnimationDuration(0.4);
                UIView.SetAnimationDelay(0.1);
                UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
            }

            _monthGridView.Alpha = 0;


            _monthGridView.Center = new CoreGraphics.CGPoint(_monthGridView.Center.X, _monthGridView.Center.Y - pointsToMove);
            gridToMove.Center = new CoreGraphics.CGPoint(gridToMove.Center.X, gridToMove.Center.Y - pointsToMove);

            _scrollView.Frame = new CoreGraphics.CGRect(
                _scrollView.Frame.Location,
                new CoreGraphics.CGSize(_scrollView.Frame.Width, (gridToMove.Lines) * DayCellHeight));

           

            _scrollView.ContentSize = _scrollView.Frame.Size;
            SetNeedsDisplay();

            if (animated)
                UIView.CommitAnimations();

            _monthGridView = gridToMove;

            UserInteractionEnabled = true;

            AdjustBackgroundColor();
        }

        /// <summary>
        /// Gos to the specified date date.
        /// </summary>
        /// <param name="targetDate">Target date.</param>
        public void GoToDate(DateTime targetDate)
        {
            if (_monthGridView == null)
                return;

            bool upwards = targetDate >= CurrentMonthYear;

            SelectedDate = targetDate.Date;
            CurrentMonthYear = new DateTime(targetDate.Year, targetDate.Month, 1);

            UserInteractionEnabled = false;

            // Dispatch event
            if (MonthChanged != null)
                MonthChanged(CurrentMonthYear);

            var gridToMove = CreateNewGrid(CurrentMonthYear);
            var pointsToMove = (upwards ? 0 + _monthGridView.Lines : 0 - _monthGridView.Lines) * DayCellHeight;

            if (upwards && gridToMove.weekdayOfFirst == 0)
                pointsToMove += DayCellHeight;
            if (!upwards && _monthGridView.weekdayOfFirst == 0)
                pointsToMove -= DayCellHeight;


            gridToMove.Frame = new CoreGraphics.CGRect(new CoreGraphics.CGPoint(0, pointsToMove), gridToMove.Frame.Size);

            _scrollView.AddSubview(gridToMove);


            _monthGridView.Center = new CoreGraphics.CGPoint(_monthGridView.Center.X, _monthGridView.Center.Y - pointsToMove);
            gridToMove.Center = new CoreGraphics.CGPoint(gridToMove.Center.X, gridToMove.Center.Y - pointsToMove);

            _scrollView.Frame = new CoreGraphics.CGRect(
                _scrollView.Frame.Location,
                new CoreGraphics.CGSize(_scrollView.Frame.Width, (gridToMove.Lines) * DayCellHeight));

          

            _monthGridView.Alpha = 0;

            _scrollView.ContentSize = _scrollView.Frame.Size;
            SetNeedsDisplay();

            _monthGridView = gridToMove;

            UserInteractionEnabled = true;

            AdjustBackgroundColor();
        }

        private void AdjustBackgroundColor()
        {
            if (_scrollView != null)
                _scrollView.BackgroundColor = MonthBackgroundColor;
            BackgroundColor = MonthBackgroundColor;
        }

        private MonthGridView CreateNewGrid(DateTime date)
        {
            var grid = new MonthGridView(this, date);
            grid.CurrentDate = CurrentDate;
            grid.BuildGrid();
            grid.Frame = MainViewSize;
            return grid;
        }

        private void LoadInitialGrids()
        {
            _monthGridView = CreateNewGrid(CurrentMonthYear);
            var rect = _scrollView.Frame;
            rect.Size = new CoreGraphics.CGSize { Height = MainViewSize.Height - HeaderViewSize.Height, Width = MainViewSize.Width };
            _scrollView.Frame = rect;
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            if (TopBar != null)
                TopBar.Draw(new CoreGraphics.CGPoint(0, 0));

            DrawDayLabels(rect);
            DrawMonthLabel(rect);
        }

        private void DrawMonthLabel(CoreGraphics.CGRect rect)
        {
            var r = new CoreGraphics.CGRect(new CoreGraphics.CGPoint(0, (HeaderViewSize.Height / 2) - 10), new CoreGraphics.CGSize { Width = HeaderViewSize.Width, Height = HeaderElementSize });
            UIColor.Black.SetColor();
            MvxTouchColor.Black1.SetColor();
            CurrentMonthYear.ToString(MonthFormatString, new CultureInfo(NSLocale.CurrentLocale.LanguageCode))
				.DrawString(r, UIFont.SystemFontOfSize(17), UILineBreakMode.WordWrap, UITextAlignment.Center);
        }

        private void DrawDayLabels(CoreGraphics.CGRect rect)
        {
            var font = UIFont.SystemFontOfSize(DayNameHeight);
            UIColor.Black.SetColor();
            var context = UIGraphics.GetCurrentContext();
            context.SaveState();
            var i = 0;

            var cultureInfo = new CultureInfo(NSLocale.CurrentLocale.LanguageCode);

            cultureInfo.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            var dayNames = cultureInfo.DateTimeFormat.DayNames;

            if (!SundayFirst)
            {
                // Shift Sunday to the end of the week
                var firstDay = dayNames[0];
                for (int count = 0; count < dayNames.Length - 1; count++)
                    dayNames[count] = dayNames[count + 1];
                dayNames[dayNames.Length - 1] = firstDay;
            }

            foreach (var d in dayNames)
            {
                d.Substring(0, 3).DrawString(new CoreGraphics.CGRect(i * DayCellWidth, HeaderViewSize.Height - DayNameHeight - 2, DayCellWidth, DayNameHeight), font, UILineBreakMode.WordWrap, UITextAlignment.Center);
                i++;
            }
            context.RestoreState();
        }

    }
}