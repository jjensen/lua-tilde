namespace Scintilla
{
    using System;
    using System.Collections;
    using System.Windows.Forms;
    using System.ComponentModel;
    
    // Function void AddStyledText(int,cells) skipped.
    // Function int GetStyledText(,textrange) skipped.
    // Function position FindText(int,findtext) skipped.
    // Function position FormatRange(bool,formatrange) skipped.
    // Function int GetTextRange(,textrange) skipped.
    public partial class ScintillaControl
    {

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="StyleNeeded"]/*' />
		private static readonly object StyleNeededEventKey = new object();  
		public event EventHandler<StyleNeededEventArgs> StyleNeeded
        {
            add { Events.AddHandler(StyleNeededEventKey, value); }
            remove { Events.RemoveHandler(StyleNeededEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="CharAdded"]/*' />
		private static readonly object CharAddedEventKey = new object();  
		public event EventHandler<CharAddedEventArgs> CharAdded
        {
            add { Events.AddHandler(CharAddedEventKey, value); }
            remove { Events.RemoveHandler(CharAddedEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="SavePointReached"]/*' />
		private static readonly object SavePointReachedEventKey = new object();  
		public event EventHandler<SavePointReachedEventArgs> SavePointReached
        {
            add { Events.AddHandler(SavePointReachedEventKey, value); }
            remove { Events.RemoveHandler(SavePointReachedEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="SavePointLeft"]/*' />
		private static readonly object SavePointLeftEventKey = new object();  
		public event EventHandler<SavePointLeftEventArgs> SavePointLeft
        {
            add { Events.AddHandler(SavePointLeftEventKey, value); }
            remove { Events.RemoveHandler(SavePointLeftEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="ModifyAttemptRO"]/*' />
		private static readonly object ModifyAttemptROEventKey = new object();  
		public event EventHandler<ModifyAttemptROEventArgs> ModifyAttemptRO
        {
            add { Events.AddHandler(ModifyAttemptROEventKey, value); }
            remove { Events.RemoveHandler(ModifyAttemptROEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="SCKey"]/*' />
		private static readonly object SCKeyEventKey = new object();  
		public event EventHandler<SCKeyEventArgs> SCKey
        {
            add { Events.AddHandler(SCKeyEventKey, value); }
            remove { Events.RemoveHandler(SCKeyEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="SCDoubleClick"]/*' />
		private static readonly object SCDoubleClickEventKey = new object();  
		public event EventHandler<SCDoubleClickEventArgs> SCDoubleClick
        {
            add { Events.AddHandler(SCDoubleClickEventKey, value); }
            remove { Events.RemoveHandler(SCDoubleClickEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="UpdateUI"]/*' />
		private static readonly object UpdateUIEventKey = new object();  
		public event EventHandler<UpdateUIEventArgs> UpdateUI
        {
            add { Events.AddHandler(UpdateUIEventKey, value); }
            remove { Events.RemoveHandler(UpdateUIEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="Modified"]/*' />
		private static readonly object ModifiedEventKey = new object();  
		public event EventHandler<ModifiedEventArgs> Modified
        {
            add { Events.AddHandler(ModifiedEventKey, value); }
            remove { Events.RemoveHandler(ModifiedEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="MacroRecord"]/*' />
		private static readonly object MacroRecordEventKey = new object();  
		public event EventHandler<MacroRecordEventArgs> MacroRecord
        {
            add { Events.AddHandler(MacroRecordEventKey, value); }
            remove { Events.RemoveHandler(MacroRecordEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="MarginClick"]/*' />
		private static readonly object MarginClickEventKey = new object();  
		public event EventHandler<MarginClickEventArgs> MarginClick
        {
            add { Events.AddHandler(MarginClickEventKey, value); }
            remove { Events.RemoveHandler(MarginClickEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="NeedShown"]/*' />
		private static readonly object NeedShownEventKey = new object();  
		public event EventHandler<NeedShownEventArgs> NeedShown
        {
            add { Events.AddHandler(NeedShownEventKey, value); }
            remove { Events.RemoveHandler(NeedShownEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="Painted"]/*' />
		private static readonly object PaintedEventKey = new object();  
		public event EventHandler<PaintedEventArgs> Painted
        {
            add { Events.AddHandler(PaintedEventKey, value); }
            remove { Events.RemoveHandler(PaintedEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="UserListSelection"]/*' />
		private static readonly object UserListSelectionEventKey = new object();  
		public event EventHandler<UserListSelectionEventArgs> UserListSelection
        {
            add { Events.AddHandler(UserListSelectionEventKey, value); }
            remove { Events.RemoveHandler(UserListSelectionEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="UriDropped"]/*' />
		private static readonly object UriDroppedEventKey = new object();  
		public event EventHandler<UriDroppedEventArgs> UriDropped
        {
            add { Events.AddHandler(UriDroppedEventKey, value); }
            remove { Events.RemoveHandler(UriDroppedEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="DwellStart"]/*' />
		private static readonly object DwellStartEventKey = new object();  
		public event EventHandler<DwellStartEventArgs> DwellStart
        {
            add { Events.AddHandler(DwellStartEventKey, value); }
            remove { Events.RemoveHandler(DwellStartEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="DwellEnd"]/*' />
		private static readonly object DwellEndEventKey = new object();  
		public event EventHandler<DwellEndEventArgs> DwellEnd
        {
            add { Events.AddHandler(DwellEndEventKey, value); }
            remove { Events.RemoveHandler(DwellEndEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="SCZoom"]/*' />
		private static readonly object SCZoomEventKey = new object();  
		public event EventHandler<SCZoomEventArgs> SCZoom
        {
            add { Events.AddHandler(SCZoomEventKey, value); }
            remove { Events.RemoveHandler(SCZoomEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="HotspotClick"]/*' />
		private static readonly object HotspotClickEventKey = new object();  
		public event EventHandler<HotspotClickEventArgs> HotspotClick
        {
            add { Events.AddHandler(HotspotClickEventKey, value); }
            remove { Events.RemoveHandler(HotspotClickEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="HotspotDoubleClick"]/*' />
		private static readonly object HotspotDoubleClickEventKey = new object();  
		public event EventHandler<HotspotDoubleClickEventArgs> HotspotDoubleClick
        {
            add { Events.AddHandler(HotspotDoubleClickEventKey, value); }
            remove { Events.RemoveHandler(HotspotDoubleClickEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="CallTipClick"]/*' />
		private static readonly object CallTipClickEventKey = new object();  
		public event EventHandler<CallTipClickEventArgs> CallTipClick
        {
            add { Events.AddHandler(CallTipClickEventKey, value); }
            remove { Events.RemoveHandler(CallTipClickEventKey, value); }
        }

		/// <include file='..\\Help\\GeneratedInclude.xml' path='root/Events/Event[@name="AutoCSelection"]/*' />
		private static readonly object AutoCSelectionEventKey = new object();  
		public event EventHandler<AutoCSelectionEventArgs> AutoCSelection
        {
            add { Events.AddHandler(AutoCSelectionEventKey, value); }
            remove { Events.RemoveHandler(AutoCSelectionEventKey, value); }
        }

        internal void DispatchScintillaEvent(SCNotification notification)
        {
            switch (notification.nmhdr.code)
            {
                    case Scintilla.Enums.Events.StyleNeeded:
                    if (Events[StyleNeededEventKey] != null)
                        ((EventHandler<StyleNeededEventArgs>)Events[StyleNeededEventKey])(this, new StyleNeededEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.CharAdded:
                    if (Events[CharAddedEventKey] != null)
                        ((EventHandler<CharAddedEventArgs>)Events[CharAddedEventKey])(this, new CharAddedEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.SavePointReached:
                    if (Events[SavePointReachedEventKey] != null)
                        ((EventHandler<SavePointReachedEventArgs>)Events[SavePointReachedEventKey])(this, new SavePointReachedEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.SavePointLeft:
                    if (Events[SavePointLeftEventKey] != null)
                        ((EventHandler<SavePointLeftEventArgs>)Events[SavePointLeftEventKey])(this, new SavePointLeftEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.ModifyAttemptRO:
                    if (Events[ModifyAttemptROEventKey] != null)
                        ((EventHandler<ModifyAttemptROEventArgs>)Events[ModifyAttemptROEventKey])(this, new ModifyAttemptROEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.SCKey:
                    if (Events[SCKeyEventKey] != null)
                        ((EventHandler<SCKeyEventArgs>)Events[SCKeyEventKey])(this, new SCKeyEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.SCDoubleClick:
                    if (Events[SCDoubleClickEventKey] != null)
                        ((EventHandler<SCDoubleClickEventArgs>)Events[SCDoubleClickEventKey])(this, new SCDoubleClickEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.UpdateUI:
                    if (Events[UpdateUIEventKey] != null)
                        ((EventHandler<UpdateUIEventArgs>)Events[UpdateUIEventKey])(this, new UpdateUIEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.Modified:
                    if (Events[ModifiedEventKey] != null)
                        ((EventHandler<ModifiedEventArgs>)Events[ModifiedEventKey])(this, new ModifiedEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.MacroRecord:
                    if (Events[MacroRecordEventKey] != null)
                        ((EventHandler<MacroRecordEventArgs>)Events[MacroRecordEventKey])(this, new MacroRecordEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.MarginClick:
                    if (Events[MarginClickEventKey] != null)
                        ((EventHandler<MarginClickEventArgs>)Events[MarginClickEventKey])(this, new MarginClickEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.NeedShown:
                    if (Events[NeedShownEventKey] != null)
                        ((EventHandler<NeedShownEventArgs>)Events[NeedShownEventKey])(this, new NeedShownEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.Painted:
                    if (Events[PaintedEventKey] != null)
                        ((EventHandler<PaintedEventArgs>)Events[PaintedEventKey])(this, new PaintedEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.UserListSelection:
                    if (Events[UserListSelectionEventKey] != null)
                        ((EventHandler<UserListSelectionEventArgs>)Events[UserListSelectionEventKey])(this, new UserListSelectionEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.UriDropped:
                    if (Events[UriDroppedEventKey] != null)
                        ((EventHandler<UriDroppedEventArgs>)Events[UriDroppedEventKey])(this, new UriDroppedEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.DwellStart:
                    if (Events[DwellStartEventKey] != null)
                        ((EventHandler<DwellStartEventArgs>)Events[DwellStartEventKey])(this, new DwellStartEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.DwellEnd:
                    if (Events[DwellEndEventKey] != null)
                        ((EventHandler<DwellEndEventArgs>)Events[DwellEndEventKey])(this, new DwellEndEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.SCZoom:
                    if (Events[SCZoomEventKey] != null)
                        ((EventHandler<SCZoomEventArgs>)Events[SCZoomEventKey])(this, new SCZoomEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.HotspotClick:
                    if (Events[HotspotClickEventKey] != null)
                        ((EventHandler<HotspotClickEventArgs>)Events[HotspotClickEventKey])(this, new HotspotClickEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.HotspotDoubleClick:
                    if (Events[HotspotDoubleClickEventKey] != null)
                        ((EventHandler<HotspotDoubleClickEventArgs>)Events[HotspotDoubleClickEventKey])(this, new HotspotDoubleClickEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.CallTipClick:
                    if (Events[CallTipClickEventKey] != null)
                        ((EventHandler<CallTipClickEventArgs>)Events[CallTipClickEventKey])(this, new CallTipClickEventArgs(notification));
                    break;

                    case Scintilla.Enums.Events.AutoCSelection:
                    if (Events[AutoCSelectionEventKey] != null)
                        ((EventHandler<AutoCSelectionEventArgs>)Events[AutoCSelectionEventKey])(this, new AutoCSelectionEventArgs(notification));
                    break;



            }
        }

        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CanRedo"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool CanRedo
        {
            get
            {
                return (this.SendMessageDirect(2016) != 0);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsAutoCActive"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsAutoCActive
        {
            get
            {
                return (this.SendMessageDirect(2102) != 0);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CanPaste"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool CanPaste
        {
            get
            {
                return (this.SendMessageDirect(2173) != 0);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CanUndo"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool CanUndo
        {
            get
            {
                return (this.SendMessageDirect(2174) != 0);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsCallTipActive"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsCallTipActive
        {
            get
            {
                return (this.SendMessageDirect(2202) != 0);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="Length"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Length
        {
            get
            {
                return this.SendMessageDirect(2006);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CurrentPos"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int CurrentPos
        {
            get
            {
                return this.SendMessageDirect(2008);
            }
            set
            {
                this.SendMessageDirect(2141, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="Anchor_"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Anchor_
        {
            get
            {
                return this.SendMessageDirect(2009);
            }
            set
            {
                this.SendMessageDirect(2026, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsUndoCollection"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsUndoCollection
        {
            get
            {
                return (this.SendMessageDirect(2019) != 0);
            }
            set
            {
                this.SendMessageDirect(2012, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="EndStyled"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int EndStyled
        {
            get
            {
                return this.SendMessageDirect(2028);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsBufferedDraw"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsBufferedDraw
        {
            get
            {
                return (this.SendMessageDirect(2034) != 0);
            }
            set
            {
                this.SendMessageDirect(2035, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="TabWidth"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int TabWidth
        {
            get
            {
                return this.SendMessageDirect(2121);
            }
            set
            {
                this.SendMessageDirect(2036, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="SelectionAlpha"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectionAlpha
        {
            get
            {
                return this.SendMessageDirect(2477);
            }
            set
            {
                this.SendMessageDirect(2478, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsSelEOLFilled"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsSelEOLFilled
        {
            get
            {
                return (this.SendMessageDirect(2479) != 0);
            }
            set
            {
                this.SendMessageDirect(2480, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CaretPeriod"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int CaretPeriod
        {
            get
            {
                return this.SendMessageDirect(2075);
            }
            set
            {
                this.SendMessageDirect(2076, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="StyleBits"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int StyleBits
        {
            get
            {
                return this.SendMessageDirect(2091);
            }
            set
            {
                this.SendMessageDirect(2090, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="MaxLineState"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int MaxLineState
        {
            get
            {
                return this.SendMessageDirect(2094);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsCaretLineVisible"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsCaretLineVisible
        {
            get
            {
                return (this.SendMessageDirect(2095) != 0);
            }
            set
            {
                this.SendMessageDirect(2096, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CaretLineBackgroundColor"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int CaretLineBackgroundColor
        {
            get
            {
                return this.SendMessageDirect(2097);
            }
            set
            {
                this.SendMessageDirect(2098, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="AutoCSeparator"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int AutoCSeparator
        {
            get
            {
                return this.SendMessageDirect(2107);
            }
            set
            {
                this.SendMessageDirect(2106, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsAutoCCancelAtStart"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsAutoCCancelAtStart
        {
            get
            {
                return (this.SendMessageDirect(2111) != 0);
            }
            set
            {
                this.SendMessageDirect(2110, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsAutoCChooseSingle"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsAutoCChooseSingle
        {
            get
            {
                return (this.SendMessageDirect(2114) != 0);
            }
            set
            {
                this.SendMessageDirect(2113, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsAutoCIgnoreCase"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsAutoCIgnoreCase
        {
            get
            {
                return (this.SendMessageDirect(2116) != 0);
            }
            set
            {
                this.SendMessageDirect(2115, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsAutoCAutoHide"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsAutoCAutoHide
        {
            get
            {
                return (this.SendMessageDirect(2119) != 0);
            }
            set
            {
                this.SendMessageDirect(2118, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsAutoCDropRestOfWord"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsAutoCDropRestOfWord
        {
            get
            {
                return (this.SendMessageDirect(2271) != 0);
            }
            set
            {
                this.SendMessageDirect(2270, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="AutoCTypeSeparator"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int AutoCTypeSeparator
        {
            get
            {
                return this.SendMessageDirect(2285);
            }
            set
            {
                this.SendMessageDirect(2286, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="AutoCMaxWidth"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int AutoCMaxWidth
        {
            get
            {
                return this.SendMessageDirect(2209);
            }
            set
            {
                this.SendMessageDirect(2208, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="AutoCMaxHeight"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int AutoCMaxHeight
        {
            get
            {
                return this.SendMessageDirect(2211);
            }
            set
            {
                this.SendMessageDirect(2210, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="Indent"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Indent
        {
            get
            {
                return this.SendMessageDirect(2123);
            }
            set
            {
                this.SendMessageDirect(2122, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsUseTabs"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsUseTabs
        {
            get
            {
                return (this.SendMessageDirect(2125) != 0);
            }
            set
            {
                this.SendMessageDirect(2124, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsHScrollBar"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsHScrollBar
        {
            get
            {
                return (this.SendMessageDirect(2131) != 0);
            }
            set
            {
                this.SendMessageDirect(2130, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsIndentationGuides"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsIndentationGuides
        {
            get
            {
                return (this.SendMessageDirect(2133) != 0);
            }
            set
            {
                this.SendMessageDirect(2132, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="HighlightGuide"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int HighlightGuide
        {
            get
            {
                return this.SendMessageDirect(2135);
            }
            set
            {
                this.SendMessageDirect(2134, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CaretFore"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int CaretFore
        {
            get
            {
                return this.SendMessageDirect(2138);
            }
            set
            {
                this.SendMessageDirect(2069, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsUsePalette"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsUsePalette
        {
            get
            {
                return (this.SendMessageDirect(2139) != 0);
            }
            set
            {
                this.SendMessageDirect(2039, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsReadOnly"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsReadOnly
        {
            get
            {
                return (this.SendMessageDirect(2140) != 0);
            }
            set
            {
                this.SendMessageDirect(2171, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="SelectionStart"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectionStart
        {
            get
            {
                return this.SendMessageDirect(2143);
            }
            set
            {
                this.SendMessageDirect(2142, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="SelectionEnd"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectionEnd
        {
            get
            {
                return this.SendMessageDirect(2145);
            }
            set
            {
                this.SendMessageDirect(2144, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="PrintMagnification"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int PrintMagnification
        {
            get
            {
                return this.SendMessageDirect(2147);
            }
            set
            {
                this.SendMessageDirect(2146, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="PrintColorMode"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int PrintColorMode
        {
            get
            {
                return this.SendMessageDirect(2149);
            }
            set
            {
                this.SendMessageDirect(2148, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="FirstVisibleLine"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int FirstVisibleLine
        {
            get
            {
                return this.SendMessageDirect(2152);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="LineCount"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int LineCount
        {
            get
            {
                return this.SendMessageDirect(2154);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="MarginLeft"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int MarginLeft
        {
            get
            {
                return this.SendMessageDirect(2156);
            }
            set
            {
                this.SendMessageDirect(2155, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="MarginRight"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int MarginRight
        {
            get
            {
                return this.SendMessageDirect(2158);
            }
            set
            {
                this.SendMessageDirect(2157, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsModify"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsModify
        {
            get
            {
                return (this.SendMessageDirect(2159) != 0);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="TextLength"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int TextLength
        {
            get
            {
                return this.SendMessageDirect(2183);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="DirectFunction"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int DirectFunction
        {
            get
            {
                return this.SendMessageDirect(2184);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="DirectPointer"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int DirectPointer
        {
            get
            {
                return this.SendMessageDirect(2185);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsOvertype"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsOvertype
        {
            get
            {
                return (this.SendMessageDirect(2187) != 0);
            }
            set
            {
                this.SendMessageDirect(2186, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CaretWidth"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int CaretWidth
        {
            get
            {
                return this.SendMessageDirect(2189);
            }
            set
            {
                this.SendMessageDirect(2188, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="TargetStart"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int TargetStart
        {
            get
            {
                return this.SendMessageDirect(2191);
            }
            set
            {
                this.SendMessageDirect(2190, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="TargetEnd"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int TargetEnd
        {
            get
            {
                return this.SendMessageDirect(2193);
            }
            set
            {
                this.SendMessageDirect(2192, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="SearchFlags"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SearchFlags
        {
            get
            {
                return this.SendMessageDirect(2199);
            }
            set
            {
                this.SendMessageDirect(2198, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsTabIndents"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsTabIndents
        {
            get
            {
                return (this.SendMessageDirect(2261) != 0);
            }
            set
            {
                this.SendMessageDirect(2260, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="BackspaceUnIndents"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool BackspaceUnIndents
        {
            get
            {
                return (this.SendMessageDirect(2263) != 0);
            }
            set
            {
                this.SendMessageDirect(2262, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="MouseDwellTime"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int MouseDwellTime
        {
            get
            {
                return this.SendMessageDirect(2265);
            }
            set
            {
                this.SendMessageDirect(2264, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="WrapMode"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int WrapMode
        {
            get
            {
                return this.SendMessageDirect(2269);
            }
            set
            {
                this.SendMessageDirect(2268, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="WrapVisualFlags"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int WrapVisualFlags
        {
            get
            {
                return this.SendMessageDirect(2461);
            }
            set
            {
                this.SendMessageDirect(2460, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="WrapVisualFlagsLocation"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int WrapVisualFlagsLocation
        {
            get
            {
                return this.SendMessageDirect(2463);
            }
            set
            {
                this.SendMessageDirect(2462, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="WrapStartIndent"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int WrapStartIndent
        {
            get
            {
                return this.SendMessageDirect(2465);
            }
            set
            {
                this.SendMessageDirect(2464, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="LayoutCache"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int LayoutCache
        {
            get
            {
                return this.SendMessageDirect(2273);
            }
            set
            {
                this.SendMessageDirect(2272, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="ScrollWidth"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int ScrollWidth
        {
            get
            {
                return this.SendMessageDirect(2275);
            }
            set
            {
                this.SendMessageDirect(2274, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsEndAtLastLine"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsEndAtLastLine
        {
            get
            {
                return (this.SendMessageDirect(2278) != 0);
            }
            set
            {
                this.SendMessageDirect(2277, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsVScrollBar"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsVScrollBar
        {
            get
            {
                return (this.SendMessageDirect(2281) != 0);
            }
            set
            {
                this.SendMessageDirect(2280, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsTwoPhaseDraw"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsTwoPhaseDraw
        {
            get
            {
                return (this.SendMessageDirect(2283) != 0);
            }
            set
            {
                this.SendMessageDirect(2284, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsViewEOL"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsViewEOL
        {
            get
            {
                return (this.SendMessageDirect(2355) != 0);
            }
            set
            {
                this.SendMessageDirect(2356, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="EdgeColumn"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int EdgeColumn
        {
            get
            {
                return this.SendMessageDirect(2360);
            }
            set
            {
                this.SendMessageDirect(2361, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="EdgeMode"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int EdgeMode
        {
            get
            {
                return this.SendMessageDirect(2362);
            }
            set
            {
                this.SendMessageDirect(2363, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="EdgeColor"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int EdgeColor
        {
            get
            {
                return this.SendMessageDirect(2364);
            }
            set
            {
                this.SendMessageDirect(2365, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="LinesOnScreen"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int LinesOnScreen
        {
            get
            {
                return this.SendMessageDirect(2370);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsSelectionIsRectangle"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsSelectionIsRectangle
        {
            get
            {
                return (this.SendMessageDirect(2372) != 0);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="Zoom"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Zoom
        {
            get
            {
                return this.SendMessageDirect(2374);
            }
            set
            {
                this.SendMessageDirect(2373, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="ModEventMask"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int ModEventMask
        {
            get
            {
                return this.SendMessageDirect(2378);
            }
            set
            {
                this.SendMessageDirect(2359, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsFocus"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsFocus
        {
            get
            {
                return (this.SendMessageDirect(2381) != 0);
            }
            set
            {
                this.SendMessageDirect(2380, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="Status"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Status
        {
            get
            {
                return this.SendMessageDirect(2383);
            }
            set
            {
                this.SendMessageDirect(2382, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsMouseDownCaptures"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsMouseDownCaptures
        {
            get
            {
                return (this.SendMessageDirect(2385) != 0);
            }
            set
            {
                this.SendMessageDirect(2384, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="Cursor_"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Cursor_
        {
            get
            {
                return this.SendMessageDirect(2387);
            }
            set
            {
                this.SendMessageDirect(2386, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="ControlCharSymbol"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int ControlCharSymbol
        {
            get
            {
                return this.SendMessageDirect(2389);
            }
            set
            {
                this.SendMessageDirect(2388, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="XOffset"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int XOffset
        {
            get
            {
                return this.SendMessageDirect(2398);
            }
            set
            {
                this.SendMessageDirect(2397, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="PrintWrapMode"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int PrintWrapMode
        {
            get
            {
                return this.SendMessageDirect(2407);
            }
            set
            {
                this.SendMessageDirect(2406, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="SelectionMode"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectionMode
        {
            get
            {
                return this.SendMessageDirect(2423);
            }
            set
            {
                this.SendMessageDirect(2422, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsCaretSticky"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsCaretSticky
        {
            get
            {
                return (this.SendMessageDirect(2457) != 0);
            }
            set
            {
                this.SendMessageDirect(2458, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="IsPasteConvertEndings"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsPasteConvertEndings
        {
            get
            {
                return (this.SendMessageDirect(2468) != 0);
            }
            set
            {
                this.SendMessageDirect(2467, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="CaretLineBackgroundAlpha"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int CaretLineBackgroundAlpha
        {
            get
            {
                return this.SendMessageDirect(2471);
            }
            set
            {
                this.SendMessageDirect(2470, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="Lexer"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Lexer
        {
            get
            {
                return this.SendMessageDirect(4002);
            }
            set
            {
                this.SendMessageDirect(4001, value);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Properties/Property[@name="StyleBitsNeeded"]/*' />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int StyleBitsNeeded
        {
            get
            {
                return this.SendMessageDirect(4011);
            }
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AddText"]/*' />
        public virtual void AddText(string text)
        {
            this.SendMessageDirect(2001, text.Length, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="InsertText"]/*' />
        public virtual void InsertText(int pos, string text)
        {
            this.SendMessageDirect(2003, pos, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ClearAll"]/*' />
        public virtual void ClearAll()
        {
            this.SendMessageDirect(2004);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ClearDocumentStyle"]/*' />
        public virtual void ClearDocumentStyle()
        {
            this.SendMessageDirect(2005);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Redo"]/*' />
        public virtual void Redo()
        {
            this.SendMessageDirect(2011);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SelectAll"]/*' />
        public virtual void SelectAll()
        {
            this.SendMessageDirect(2013);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetSavePoint"]/*' />
        public virtual void SetSavePoint()
        {
            this.SendMessageDirect(2014);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerLineFromHandle"]/*' />
        public virtual int MarkerLineFromHandle(int handle)
        {
            return this.SendMessageDirect(2017, handle);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerDeleteHandle"]/*' />
        public virtual void MarkerDeleteHandle(int handle)
        {
            this.SendMessageDirect(2018, handle);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PositionFromPoint"]/*' />
        public virtual int PositionFromPoint(int x, int y)
        {
            return this.SendMessageDirect(2022, x, y);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PositionFromPointClose"]/*' />
        public virtual int PositionFromPointClose(int x, int y)
        {
            return this.SendMessageDirect(2023, x, y);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GotoLine"]/*' />
        public virtual void GotoLine(int line)
        {
            this.SendMessageDirect(2024, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GotoPos"]/*' />
        public virtual void GotoPos(int pos)
        {
            this.SendMessageDirect(2025, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GetCurLine"]/*' />
        public virtual string GetCurLine()
        {
            string result;
            this.SendMessageDirect(2027, out result);
            return result;
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StartStyling"]/*' />
        public virtual void StartStyling(int pos, int mask)
        {
            this.SendMessageDirect(2032, pos, mask);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetStyling"]/*' />
        public virtual void SetStyling(int length, int style)
        {
            this.SendMessageDirect(2033, length, style);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerSetForegroundColor"]/*' />
        public virtual void MarkerSetForegroundColor(int markerNumber, int fore)
        {
            this.SendMessageDirect(2041, markerNumber, fore);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerSetBackgroundColor"]/*' />
        public virtual void MarkerSetBackgroundColor(int markerNumber, int back)
        {
            this.SendMessageDirect(2042, markerNumber, back);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerAdd"]/*' />
        public virtual int MarkerAdd(int line, int markerNumber)
        {
            return this.SendMessageDirect(2043, line, markerNumber);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerDelete"]/*' />
        public virtual void MarkerDelete(int line, int markerNumber)
        {
            this.SendMessageDirect(2044, line, markerNumber);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerDeleteAll"]/*' />
        public virtual void MarkerDeleteAll(int markerNumber)
        {
            this.SendMessageDirect(2045, markerNumber);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerGet"]/*' />
        public virtual int MarkerGet(int line)
        {
            return this.SendMessageDirect(2046, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerNext"]/*' />
        public virtual int MarkerNext(int lineStart, int markerMask)
        {
            return this.SendMessageDirect(2047, lineStart, markerMask);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerPrevious"]/*' />
        public virtual int MarkerPrevious(int lineStart, int markerMask)
        {
            return this.SendMessageDirect(2048, lineStart, markerMask);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerDefinePixmap"]/*' />
        public virtual void MarkerDefinePixmap(int markerNumber, string pixmap)
        {
            this.SendMessageDirect(2049, markerNumber, pixmap);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerAddSet"]/*' />
        public virtual void MarkerAddSet(int line, int set)
        {
            this.SendMessageDirect(2466, line, set);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarkerSetAlpha"]/*' />
        public virtual void MarkerSetAlpha(int markerNumber, int alpha)
        {
            this.SendMessageDirect(2476, markerNumber, alpha);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleResetDefault"]/*' />
        public virtual void StyleResetDefault()
        {
            this.SendMessageDirect(2058);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetSelectionForeground"]/*' />
        public virtual void SetSelectionForeground(bool useSetting, int fore)
        {
            this.SendMessageDirect(2067, useSetting, fore);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetSelectionBackground"]/*' />
        public virtual void SetSelectionBackground(bool useSetting, int back)
        {
            this.SendMessageDirect(2068, useSetting, back);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ClearAllCmdKeys"]/*' />
        public virtual void ClearAllCmdKeys()
        {
            this.SendMessageDirect(2072);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetStylingEx"]/*' />
        public virtual void SetStylingEx(string styles)
        {
            this.SendMessageDirect(2073, styles.Length, styles);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="BeginUndoAction"]/*' />
        public virtual void BeginUndoAction()
        {
            this.SendMessageDirect(2078);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="EndUndoAction"]/*' />
        public virtual void EndUndoAction()
        {
            this.SendMessageDirect(2079);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetWhiteSpaceForeground"]/*' />
        public virtual void SetWhiteSpaceForeground(bool useSetting, int fore)
        {
            this.SendMessageDirect(2084, useSetting, fore);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetWhiteSpaceBackground"]/*' />
        public virtual void SetWhiteSpaceBackground(bool useSetting, int back)
        {
            this.SendMessageDirect(2085, useSetting, back);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCShow"]/*' />
        public virtual void AutoCShow(int lenEntered, string itemList)
        {
            this.SendMessageDirect(2100, lenEntered, itemList);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCCancel"]/*' />
        public virtual void AutoCCancel()
        {
            this.SendMessageDirect(2101);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCPosStart"]/*' />
        public virtual int AutoCPosStart()
        {
            return this.SendMessageDirect(2103);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCComplete"]/*' />
        public virtual void AutoCComplete()
        {
            this.SendMessageDirect(2104);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCStops"]/*' />
        public virtual void AutoCStops(string characterSet)
        {
            this.SendMessageDirect(2105, VOID.NULL, characterSet);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCSelect"]/*' />
        public virtual void AutoCSelect(string text)
        {
            this.SendMessageDirect(2108, VOID.NULL, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="UserListShow"]/*' />
        public virtual void UserListShow(int listType, string itemList)
        {
            this.SendMessageDirect(2117, listType, itemList);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="RegisterImage"]/*' />
        public virtual void RegisterImage(int type, string xpmData)
        {
            this.SendMessageDirect(2405, type, xpmData);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ClearRegisteredImages"]/*' />
        public virtual void ClearRegisteredImages()
        {
            this.SendMessageDirect(2408);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetSelection"]/*' />
        public virtual void SetSelection(int start, int end)
        {
            this.SendMessageDirect(2160, start, end);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GetSelectedText"]/*' />
        public virtual string GetSelectedText()
        {
            string result;
            this.SendMessageDirect(2161, out result);
            return result;
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HideSelection"]/*' />
        public virtual void HideSelection(bool normal)
        {
            this.SendMessageDirect(2163, normal);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PointXFromPosition"]/*' />
        public virtual int PointXFromPosition(int pos)
        {
            return this.SendMessageDirect(2164, VOID.NULL, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PointYFromPosition"]/*' />
        public virtual int PointYFromPosition(int pos)
        {
            return this.SendMessageDirect(2165, VOID.NULL, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineFromPosition"]/*' />
        public virtual int LineFromPosition(int pos)
        {
            return this.SendMessageDirect(2166, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PositionFromLine"]/*' />
        public virtual int PositionFromLine(int line)
        {
            return this.SendMessageDirect(2167, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineScroll"]/*' />
        public virtual void LineScroll(int columns, int lines)
        {
            this.SendMessageDirect(2168, columns, lines);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ScrollCaret"]/*' />
        public virtual void ScrollCaret()
        {
            this.SendMessageDirect(2169);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ReplaceSelection"]/*' />
        public virtual void ReplaceSelection(string text)
        {
            this.SendMessageDirect(2170, VOID.NULL, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Null"]/*' />
        public virtual void Null()
        {
            this.SendMessageDirect(2172);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="EmptyUndoBuffer"]/*' />
        public virtual void EmptyUndoBuffer()
        {
            this.SendMessageDirect(2175);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Undo"]/*' />
        public virtual void Undo()
        {
            this.SendMessageDirect(2176);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Cut"]/*' />
        public virtual void Cut()
        {
            this.SendMessageDirect(2177);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Copy"]/*' />
        public virtual void Copy()
        {
            this.SendMessageDirect(2178);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Paste"]/*' />
        public virtual void Paste()
        {
            this.SendMessageDirect(2179);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Clear"]/*' />
        public virtual void Clear()
        {
            this.SendMessageDirect(2180);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetText"]/*' />
        public virtual void SetText(string text)
        {
            this.SendMessageDirect(2181, VOID.NULL, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ReplaceTarget"]/*' />
        public virtual int ReplaceTarget(string text)
        {
            return this.SendMessageDirect(2194, text.Length, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ReplaceTargetRE"]/*' />
        public virtual int ReplaceTargetRE(string text)
        {
            return this.SendMessageDirect(2195, text.Length, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SearchInTarget"]/*' />
        public virtual int SearchInTarget(string text)
        {
            return this.SendMessageDirect(2197, text.Length, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipShow"]/*' />
        public virtual void CallTipShow(int pos, string definition)
        {
            this.SendMessageDirect(2200, pos, definition);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipCancel"]/*' />
        public virtual void CallTipCancel()
        {
            this.SendMessageDirect(2201);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipPosStart"]/*' />
        public virtual int CallTipPosStart()
        {
            return this.SendMessageDirect(2203);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipSetHlt"]/*' />
        public virtual void CallTipSetHlt(int start, int end)
        {
            this.SendMessageDirect(2204, start, end);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="VisibleFromDocLine"]/*' />
        public virtual int VisibleFromDocLine(int line)
        {
            return this.SendMessageDirect(2220, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DocLineFromVisible"]/*' />
        public virtual int DocLineFromVisible(int lineDisplay)
        {
            return this.SendMessageDirect(2221, lineDisplay);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WrapCount"]/*' />
        public virtual int WrapCount(int line)
        {
            return this.SendMessageDirect(2235, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ShowLines"]/*' />
        public virtual void ShowLines(int lineStart, int lineEnd)
        {
            this.SendMessageDirect(2226, lineStart, lineEnd);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HideLines"]/*' />
        public virtual void HideLines(int lineStart, int lineEnd)
        {
            this.SendMessageDirect(2227, lineStart, lineEnd);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ToggleFold"]/*' />
        public virtual void ToggleFold(int line)
        {
            this.SendMessageDirect(2231, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="EnsureVisible"]/*' />
        public virtual void EnsureVisible(int line)
        {
            this.SendMessageDirect(2232, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetFoldFlags"]/*' />
        public virtual void SetFoldFlags(int flags)
        {
            this.SendMessageDirect(2233, flags);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="EnsureVisibleEnforcePolicy"]/*' />
        public virtual void EnsureVisibleEnforcePolicy(int line)
        {
            this.SendMessageDirect(2234, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordStartPosition"]/*' />
        public virtual int WordStartPosition(int pos, bool onlyWordCharacters)
        {
            return this.SendMessageDirect(2266, pos, onlyWordCharacters);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordEndPosition"]/*' />
        public virtual int WordEndPosition(int pos, bool onlyWordCharacters)
        {
            return this.SendMessageDirect(2267, pos, onlyWordCharacters);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="TextWidth"]/*' />
        public virtual int TextWidth(int style, string text)
        {
            return this.SendMessageDirect(2276, style, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="TextHeight"]/*' />
        public virtual int TextHeight(int line)
        {
            return this.SendMessageDirect(2279, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AppendText"]/*' />
        public virtual void AppendText(string text)
        {
            this.SendMessageDirect(2282, text.Length, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="TargetFromSelection"]/*' />
        public virtual void TargetFromSelection()
        {
            this.SendMessageDirect(2287);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LinesJoin"]/*' />
        public virtual void LinesJoin()
        {
            this.SendMessageDirect(2288);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LinesSplit"]/*' />
        public virtual void LinesSplit(int pixelWidth)
        {
            this.SendMessageDirect(2289, pixelWidth);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetFoldMarginColor"]/*' />
        public virtual void SetFoldMarginColor(bool useSetting, int back)
        {
            this.SendMessageDirect(2290, useSetting, back);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetFoldMarginHiColor"]/*' />
        public virtual void SetFoldMarginHiColor(bool useSetting, int fore)
        {
            this.SendMessageDirect(2291, useSetting, fore);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineDown"]/*' />
        public virtual void LineDown()
        {
            this.SendMessageDirect(2300);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineDownExtend"]/*' />
        public virtual void LineDownExtend()
        {
            this.SendMessageDirect(2301);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineUp"]/*' />
        public virtual void LineUp()
        {
            this.SendMessageDirect(2302);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineUpExtend"]/*' />
        public virtual void LineUpExtend()
        {
            this.SendMessageDirect(2303);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CharLeft"]/*' />
        public virtual void CharLeft()
        {
            this.SendMessageDirect(2304);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CharLeftExtend"]/*' />
        public virtual void CharLeftExtend()
        {
            this.SendMessageDirect(2305);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CharRight"]/*' />
        public virtual void CharRight()
        {
            this.SendMessageDirect(2306);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CharRightExtend"]/*' />
        public virtual void CharRightExtend()
        {
            this.SendMessageDirect(2307);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordLeft"]/*' />
        public virtual void WordLeft()
        {
            this.SendMessageDirect(2308);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordLeftExtend"]/*' />
        public virtual void WordLeftExtend()
        {
            this.SendMessageDirect(2309);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordRight"]/*' />
        public virtual void WordRight()
        {
            this.SendMessageDirect(2310);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordRightExtend"]/*' />
        public virtual void WordRightExtend()
        {
            this.SendMessageDirect(2311);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Home"]/*' />
        public virtual void Home()
        {
            this.SendMessageDirect(2312);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HomeExtend"]/*' />
        public virtual void HomeExtend()
        {
            this.SendMessageDirect(2313);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEnd"]/*' />
        public virtual void LineEnd()
        {
            this.SendMessageDirect(2314);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEndExtend"]/*' />
        public virtual void LineEndExtend()
        {
            this.SendMessageDirect(2315);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DocumentStart"]/*' />
        public virtual void DocumentStart()
        {
            this.SendMessageDirect(2316);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DocumentStartExtend"]/*' />
        public virtual void DocumentStartExtend()
        {
            this.SendMessageDirect(2317);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DocumentEnd"]/*' />
        public virtual void DocumentEnd()
        {
            this.SendMessageDirect(2318);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DocumentEndExtend"]/*' />
        public virtual void DocumentEndExtend()
        {
            this.SendMessageDirect(2319);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PageUp"]/*' />
        public virtual void PageUp()
        {
            this.SendMessageDirect(2320);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PageUpExtend"]/*' />
        public virtual void PageUpExtend()
        {
            this.SendMessageDirect(2321);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PageDown"]/*' />
        public virtual void PageDown()
        {
            this.SendMessageDirect(2322);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PageDownExtend"]/*' />
        public virtual void PageDownExtend()
        {
            this.SendMessageDirect(2323);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="EditToggleOvertype"]/*' />
        public virtual void EditToggleOvertype()
        {
            this.SendMessageDirect(2324);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Cancel"]/*' />
        public virtual void Cancel()
        {
            this.SendMessageDirect(2325);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DeleteBack"]/*' />
        public virtual void DeleteBack()
        {
            this.SendMessageDirect(2326);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Tab"]/*' />
        public virtual void Tab()
        {
            this.SendMessageDirect(2327);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="BackTab"]/*' />
        public virtual void BackTab()
        {
            this.SendMessageDirect(2328);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="NewLine"]/*' />
        public virtual void NewLine()
        {
            this.SendMessageDirect(2329);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="FormFeed"]/*' />
        public virtual void FormFeed()
        {
            this.SendMessageDirect(2330);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="VCHome"]/*' />
        public virtual void VCHome()
        {
            this.SendMessageDirect(2331);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="VCHomeExtend"]/*' />
        public virtual void VCHomeExtend()
        {
            this.SendMessageDirect(2332);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ZoomIn"]/*' />
        public virtual void ZoomIn()
        {
            this.SendMessageDirect(2333);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ZoomOut"]/*' />
        public virtual void ZoomOut()
        {
            this.SendMessageDirect(2334);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DelWordLeft"]/*' />
        public virtual void DelWordLeft()
        {
            this.SendMessageDirect(2335);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DelWordRight"]/*' />
        public virtual void DelWordRight()
        {
            this.SendMessageDirect(2336);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineCut"]/*' />
        public virtual void LineCut()
        {
            this.SendMessageDirect(2337);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineDelete"]/*' />
        public virtual void LineDelete()
        {
            this.SendMessageDirect(2338);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineTranspose"]/*' />
        public virtual void LineTranspose()
        {
            this.SendMessageDirect(2339);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineDuplicate"]/*' />
        public virtual void LineDuplicate()
        {
            this.SendMessageDirect(2404);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Lowercase"]/*' />
        public virtual void Lowercase()
        {
            this.SendMessageDirect(2340);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Uppercase"]/*' />
        public virtual void Uppercase()
        {
            this.SendMessageDirect(2341);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineScrollDown"]/*' />
        public virtual void LineScrollDown()
        {
            this.SendMessageDirect(2342);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineScrollUp"]/*' />
        public virtual void LineScrollUp()
        {
            this.SendMessageDirect(2343);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DeleteBackNotLine"]/*' />
        public virtual void DeleteBackNotLine()
        {
            this.SendMessageDirect(2344);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HomeDisplay"]/*' />
        public virtual void HomeDisplay()
        {
            this.SendMessageDirect(2345);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HomeDisplayExtend"]/*' />
        public virtual void HomeDisplayExtend()
        {
            this.SendMessageDirect(2346);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEndDisplay"]/*' />
        public virtual void LineEndDisplay()
        {
            this.SendMessageDirect(2347);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEndDisplayExtend"]/*' />
        public virtual void LineEndDisplayExtend()
        {
            this.SendMessageDirect(2348);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HomeWrap"]/*' />
        public virtual void HomeWrap()
        {
            this.SendMessageDirect(2349);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HomeWrapExtend"]/*' />
        public virtual void HomeWrapExtend()
        {
            this.SendMessageDirect(2450);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEndWrap"]/*' />
        public virtual void LineEndWrap()
        {
            this.SendMessageDirect(2451);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEndWrapExtend"]/*' />
        public virtual void LineEndWrapExtend()
        {
            this.SendMessageDirect(2452);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="VCHomeWrap"]/*' />
        public virtual void VCHomeWrap()
        {
            this.SendMessageDirect(2453);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="VCHomeWrapExtend"]/*' />
        public virtual void VCHomeWrapExtend()
        {
            this.SendMessageDirect(2454);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineCopy"]/*' />
        public virtual void LineCopy()
        {
            this.SendMessageDirect(2455);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MoveCaretInsideView"]/*' />
        public virtual void MoveCaretInsideView()
        {
            this.SendMessageDirect(2401);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineLength"]/*' />
        public virtual int LineLength(int line)
        {
            return this.SendMessageDirect(2350, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="BraceHighlight"]/*' />
        public virtual void BraceHighlight(int pos1, int pos2)
        {
            this.SendMessageDirect(2351, pos1, pos2);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="BraceBadLight"]/*' />
        public virtual void BraceBadLight(int pos)
        {
            this.SendMessageDirect(2352, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="BraceMatch"]/*' />
        public virtual int BraceMatch(int pos)
        {
            return this.SendMessageDirect(2353, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SearchAnchor"]/*' />
        public virtual void SearchAnchor()
        {
            this.SendMessageDirect(2366);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SearchNext"]/*' />
        public virtual int SearchNext(int flags, string text)
        {
            return this.SendMessageDirect(2367, flags, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SearchPrevious"]/*' />
        public virtual int SearchPrevious(int flags, string text)
        {
            return this.SendMessageDirect(2368, flags, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="UsePopup"]/*' />
        public virtual void UsePopup(bool allowPopUp)
        {
            this.SendMessageDirect(2371, allowPopUp);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordPartLeft"]/*' />
        public virtual void WordPartLeft()
        {
            this.SendMessageDirect(2390);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordPartLeftExtend"]/*' />
        public virtual void WordPartLeftExtend()
        {
            this.SendMessageDirect(2391);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordPartRight"]/*' />
        public virtual void WordPartRight()
        {
            this.SendMessageDirect(2392);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordPartRightExtend"]/*' />
        public virtual void WordPartRightExtend()
        {
            this.SendMessageDirect(2393);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetVisiblePolicy"]/*' />
        public virtual void SetVisiblePolicy(int visiblePolicy, int visibleSlop)
        {
            this.SendMessageDirect(2394, visiblePolicy, visibleSlop);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DelLineLeft"]/*' />
        public virtual void DelLineLeft()
        {
            this.SendMessageDirect(2395);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="DelLineRight"]/*' />
        public virtual void DelLineRight()
        {
            this.SendMessageDirect(2396);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ChooseCaretX"]/*' />
        public virtual void ChooseCaretX()
        {
            this.SendMessageDirect(2399);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GrabFocus"]/*' />
        public virtual void GrabFocus()
        {
            this.SendMessageDirect(2400);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetXCaretPolicy"]/*' />
        public virtual void SetXCaretPolicy(int caretPolicy, int caretSlop)
        {
            this.SendMessageDirect(2402, caretPolicy, caretSlop);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetYCaretPolicy"]/*' />
        public virtual void SetYCaretPolicy(int caretPolicy, int caretSlop)
        {
            this.SendMessageDirect(2403, caretPolicy, caretSlop);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ParaDown"]/*' />
        public virtual void ParaDown()
        {
            this.SendMessageDirect(2413);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ParaDownExtend"]/*' />
        public virtual void ParaDownExtend()
        {
            this.SendMessageDirect(2414);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ParaUp"]/*' />
        public virtual void ParaUp()
        {
            this.SendMessageDirect(2415);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ParaUpExtend"]/*' />
        public virtual void ParaUpExtend()
        {
            this.SendMessageDirect(2416);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PositionBefore"]/*' />
        public virtual int PositionBefore(int pos)
        {
            return this.SendMessageDirect(2417, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PositionAfter"]/*' />
        public virtual int PositionAfter(int pos)
        {
            return this.SendMessageDirect(2418, pos);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CopyRange"]/*' />
        public virtual void CopyRange(int start, int end)
        {
            this.SendMessageDirect(2419, start, end);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CopyText"]/*' />
        public virtual void CopyText(string text)
        {
            this.SendMessageDirect(2420, text.Length, text);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GetLineSelectionStartPosition"]/*' />
        public virtual int GetLineSelectionStartPosition(int line)
        {
            return this.SendMessageDirect(2424, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GetLineSelectionEndPosition"]/*' />
        public virtual int GetLineSelectionEndPosition(int line)
        {
            return this.SendMessageDirect(2425, line);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineDownRectExtend"]/*' />
        public virtual void LineDownRectExtend()
        {
            this.SendMessageDirect(2426);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineUpRectExtend"]/*' />
        public virtual void LineUpRectExtend()
        {
            this.SendMessageDirect(2427);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CharLeftRectExtend"]/*' />
        public virtual void CharLeftRectExtend()
        {
            this.SendMessageDirect(2428);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CharRightRectExtend"]/*' />
        public virtual void CharRightRectExtend()
        {
            this.SendMessageDirect(2429);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HomeRectExtend"]/*' />
        public virtual void HomeRectExtend()
        {
            this.SendMessageDirect(2430);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="VCHomeRectExtend"]/*' />
        public virtual void VCHomeRectExtend()
        {
            this.SendMessageDirect(2431);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEndRectExtend"]/*' />
        public virtual void LineEndRectExtend()
        {
            this.SendMessageDirect(2432);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PageUpRectExtend"]/*' />
        public virtual void PageUpRectExtend()
        {
            this.SendMessageDirect(2433);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PageDownRectExtend"]/*' />
        public virtual void PageDownRectExtend()
        {
            this.SendMessageDirect(2434);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StutteredPageUp"]/*' />
        public virtual void StutteredPageUp()
        {
            this.SendMessageDirect(2435);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StutteredPageUpExtend"]/*' />
        public virtual void StutteredPageUpExtend()
        {
            this.SendMessageDirect(2436);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StutteredPageDown"]/*' />
        public virtual void StutteredPageDown()
        {
            this.SendMessageDirect(2437);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StutteredPageDownExtend"]/*' />
        public virtual void StutteredPageDownExtend()
        {
            this.SendMessageDirect(2438);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordLeftEnd"]/*' />
        public virtual void WordLeftEnd()
        {
            this.SendMessageDirect(2439);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordLeftEndExtend"]/*' />
        public virtual void WordLeftEndExtend()
        {
            this.SendMessageDirect(2440);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordRightEnd"]/*' />
        public virtual void WordRightEnd()
        {
            this.SendMessageDirect(2441);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordRightEndExtend"]/*' />
        public virtual void WordRightEndExtend()
        {
            this.SendMessageDirect(2442);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetCharsDefault"]/*' />
        public virtual void SetCharsDefault()
        {
            this.SendMessageDirect(2444);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCGetCurrent"]/*' />
        public virtual int AutoCGetCurrent()
        {
            return this.SendMessageDirect(2445);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Allocate"]/*' />
        public virtual void Allocate(int bytes)
        {
            this.SendMessageDirect(2446, bytes);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="TargetAsUTF8"]/*' />
        public virtual string TargetAsUTF8()
        {
            string result;
            this.SendMessageDirect(2447, out result);
            return result;
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SetLengthForEncode"]/*' />
        public virtual void SetLengthForEncode(int bytes)
        {
            this.SendMessageDirect(2448, bytes);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="EncodedFromUTF8"]/*' />
        public virtual string EncodedFromUTF8(string utf8)
        {
            string result;
            this.SendMessageDirect(2449, utf8, out result);
            return result;
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="FindColumn"]/*' />
        public virtual int FindColumn(int line, int column)
        {
            return this.SendMessageDirect(2456, line, column);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="ToggleCaretSticky"]/*' />
        public virtual void ToggleCaretSticky()
        {
            this.SendMessageDirect(2459);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="SelectionDuplicate"]/*' />
        public virtual void SelectionDuplicate()
        {
            this.SendMessageDirect(2469);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StartRecord"]/*' />
        public virtual void StartRecord()
        {
            this.SendMessageDirect(3001);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StopRecord"]/*' />
        public virtual void StopRecord()
        {
            this.SendMessageDirect(3002);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Colorize"]/*' />
        public virtual void Colorize(int start, int end)
        {
            this.SendMessageDirect(4003, start, end);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LoadLexerLibrary"]/*' />
        public virtual void LoadLexerLibrary(string path)
        {
            this.SendMessageDirect(4007, VOID.NULL, path);
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GetProperty"]/*' />
        public virtual string GetProperty(string key)
        {
            string result;
            this.SendMessageDirect(4008, key, out result);
            return result;
        }
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="GetPropertyExpanded"]/*' />
        public virtual string GetPropertyExpanded(string key)
        {
            string result;
            this.SendMessageDirect(4009, key, out result);
            return result;
        }
        // NOTE: originally a property:StyleAt
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleAt"]/*' />
        public virtual int StyleAt(int pos)
        {
            return this.SendMessageDirect(2010, pos);
        }
        // NOTE: originally a property:MarginTypeN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginTypeN"]/*' />
        public virtual int MarginTypeN(int margin)
        {
            return this.SendMessageDirect(2241, margin);
        }
        // NOTE: originally a property:MarginWidthN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginWidthN"]/*' />
        public virtual int MarginWidthN(int margin)
        {
            return this.SendMessageDirect(2243, margin);
        }
        // NOTE: originally a property:MarginMaskN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginMaskN"]/*' />
        public virtual int MarginMaskN(int margin)
        {
            return this.SendMessageDirect(2245, margin);
        }
        // NOTE: originally a property:MarginSensitiveN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginSensitiveN"]/*' />
        public virtual bool MarginSensitiveN(int margin)
        {
            return (this.SendMessageDirect(2247, margin) != 0);
        }
        // NOTE: originally a property:IndicGetFore
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="IndicGetFore"]/*' />
        public virtual int IndicGetFore(int indic)
        {
            return this.SendMessageDirect(2083, indic);
        }
        // NOTE: originally a property:LineState
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineState"]/*' />
        public virtual int LineState(int line)
        {
            return this.SendMessageDirect(2093, line);
        }
        // NOTE: originally a property:LineIndentation
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineIndentation"]/*' />
        public virtual int LineIndentation(int line)
        {
            return this.SendMessageDirect(2127, line);
        }
        // NOTE: originally a property:LineIndentPosition
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineIndentPosition"]/*' />
        public virtual int LineIndentPosition(int line)
        {
            return this.SendMessageDirect(2128, line);
        }
        // NOTE: originally a property:Column
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Column"]/*' />
        public virtual int Column(int pos)
        {
            return this.SendMessageDirect(2129, pos);
        }
        // NOTE: originally a property:LineEndPosition
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineEndPosition"]/*' />
        public virtual int LineEndPosition(int line)
        {
            return this.SendMessageDirect(2136, line);
        }
        // NOTE: originally a property:FoldLevel
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="FoldLevel"]/*' />
        public virtual int FoldLevel(int line)
        {
            return this.SendMessageDirect(2223, line);
        }
        // NOTE: originally a property:LastChild
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LastChild"]/*' />
        public virtual int LastChild(int line, int level)
        {
            return this.SendMessageDirect(2224, line, level);
        }
        // NOTE: originally a property:FoldParent
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="FoldParent"]/*' />
        public virtual int FoldParent(int line)
        {
            return this.SendMessageDirect(2225, line);
        }
        // NOTE: originally a property:LineVisible
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineVisible"]/*' />
        public virtual bool LineVisible(int line)
        {
            return (this.SendMessageDirect(2228, line) != 0);
        }
        // NOTE: originally a property:FoldExpanded
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="FoldExpanded"]/*' />
        public virtual bool FoldExpanded(int line)
        {
            return (this.SendMessageDirect(2230, line) != 0);
        }
        // NOTE: originally a property:PropertyInt
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="PropertyInt"]/*' />
        public virtual int PropertyInt(string key)
        {
            return this.SendMessageDirect(4010, key);
        }
        // NOTE: originally a property:MarginTypeN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginTypeN"]/*' />
        public virtual void MarginTypeN(int margin, int marginType)
        {
            this.SendMessageDirect(2240, margin, marginType);
        }
        // NOTE: originally a property:MarginWidthN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginWidthN"]/*' />
        public virtual void MarginWidthN(int margin, int pixelWidth)
        {
            this.SendMessageDirect(2242, margin, pixelWidth);
        }
        // NOTE: originally a property:MarginMaskN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginMaskN"]/*' />
        public virtual void MarginMaskN(int margin, int mask)
        {
            this.SendMessageDirect(2244, margin, mask);
        }
        // NOTE: originally a property:MarginSensitiveN
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="MarginSensitiveN"]/*' />
        public virtual void MarginSensitiveN(int margin, bool sensitive)
        {
            this.SendMessageDirect(2246, margin, sensitive);
        }
        // NOTE: originally a property:StyleClearAll
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleClearAll"]/*' />
        public virtual void StyleClearAll()
        {
            this.SendMessageDirect(2050);
        }
        // NOTE: originally a property:StyleSetFore
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetFore"]/*' />
        public virtual void StyleSetFore(int style, int fore)
        {
            this.SendMessageDirect(2051, style, fore);
        }
        // NOTE: originally a property:StyleSetBack
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetBack"]/*' />
        public virtual void StyleSetBack(int style, int back)
        {
            this.SendMessageDirect(2052, style, back);
        }
        // NOTE: originally a property:StyleSetBold
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetBold"]/*' />
        public virtual void StyleSetBold(int style, bool bold)
        {
            this.SendMessageDirect(2053, style, bold);
        }
        // NOTE: originally a property:StyleSetItalic
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetItalic"]/*' />
        public virtual void StyleSetItalic(int style, bool italic)
        {
            this.SendMessageDirect(2054, style, italic);
        }
        // NOTE: originally a property:StyleSetSize
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetSize"]/*' />
        public virtual void StyleSetSize(int style, int sizePoints)
        {
            this.SendMessageDirect(2055, style, sizePoints);
        }
        // NOTE: originally a property:StyleSetFont
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetFont"]/*' />
        public virtual void StyleSetFont(int style, string fontName)
        {
            this.SendMessageDirect(2056, style, fontName);
        }
        // NOTE: originally a property:StyleSetEOLFilled
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetEOLFilled"]/*' />
        public virtual void StyleSetEOLFilled(int style, bool filled)
        {
            this.SendMessageDirect(2057, style, filled);
        }
        // NOTE: originally a property:StyleSetUnderline
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetUnderline"]/*' />
        public virtual void StyleSetUnderline(int style, bool underline)
        {
            this.SendMessageDirect(2059, style, underline);
        }
        // NOTE: originally a property:StyleSetHotSpot
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetHotSpot"]/*' />
        public virtual void StyleSetHotSpot(int style, bool hotspot)
        {
            this.SendMessageDirect(2409, style, hotspot);
        }
        // NOTE: originally a property:StyleSetVisible
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetVisible"]/*' />
        public virtual void StyleSetVisible(int style, bool visible)
        {
            this.SendMessageDirect(2074, style, visible);
        }
        // NOTE: originally a property:WordChars
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WordChars"]/*' />
        public virtual void WordChars(string characters)
        {
            this.SendMessageDirect(2077, VOID.NULL, characters);
        }
        // NOTE: originally a property:LineState
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineState"]/*' />
        public virtual void LineState(int line, int state)
        {
            this.SendMessageDirect(2092, line, state);
        }
        // NOTE: originally a property:StyleSetChangeable
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="StyleSetChangeable"]/*' />
        public virtual void StyleSetChangeable(int style, bool changeable)
        {
            this.SendMessageDirect(2099, style, changeable);
        }
        // NOTE: originally a property:AutoCSetFillUps
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="AutoCSetFillUps"]/*' />
        public virtual void AutoCSetFillUps(string characterSet)
        {
            this.SendMessageDirect(2112, VOID.NULL, characterSet);
        }
        // NOTE: originally a property:LineIndentation
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LineIndentation"]/*' />
        public virtual void LineIndentation(int line, int indentSize)
        {
            this.SendMessageDirect(2126, line, indentSize);
        }
        // NOTE: originally a property:CallTipSetBack
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipSetBack"]/*' />
        public virtual void CallTipSetBack(int back)
        {
            this.SendMessageDirect(2205, back);
        }
        // NOTE: originally a property:CallTipSetFore
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipSetFore"]/*' />
        public virtual void CallTipSetFore(int fore)
        {
            this.SendMessageDirect(2206, fore);
        }
        // NOTE: originally a property:CallTipSetForeHlt
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipSetForeHlt"]/*' />
        public virtual void CallTipSetForeHlt(int fore)
        {
            this.SendMessageDirect(2207, fore);
        }
        // NOTE: originally a property:CallTipUseStyle
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="CallTipUseStyle"]/*' />
        public virtual void CallTipUseStyle(int tabSize)
        {
            this.SendMessageDirect(2212, tabSize);
        }
        // NOTE: originally a property:FoldLevel
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="FoldLevel"]/*' />
        public virtual void FoldLevel(int line, int level)
        {
            this.SendMessageDirect(2222, line, level);
        }
        // NOTE: originally a property:FoldExpanded
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="FoldExpanded"]/*' />
        public virtual void FoldExpanded(int line, bool expanded)
        {
            this.SendMessageDirect(2229, line, expanded);
        }
        // NOTE: originally a property:HotspotActiveFore
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HotspotActiveFore"]/*' />
        public virtual void HotspotActiveFore(bool useSetting, int fore)
        {
            this.SendMessageDirect(2410, useSetting, fore);
        }
        // NOTE: originally a property:HotspotActiveBack
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HotspotActiveBack"]/*' />
        public virtual void HotspotActiveBack(bool useSetting, int back)
        {
            this.SendMessageDirect(2411, useSetting, back);
        }
        // NOTE: originally a property:HotspotActiveUnderline
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HotspotActiveUnderline"]/*' />
        public virtual void HotspotActiveUnderline(bool underline)
        {
            this.SendMessageDirect(2412, underline);
        }
        // NOTE: originally a property:HotspotSingleLine
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="HotspotSingleLine"]/*' />
        public virtual void HotspotSingleLine(bool singleLine)
        {
            this.SendMessageDirect(2421, singleLine);
        }
        // NOTE: originally a property:WhitespaceChars
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="WhitespaceChars"]/*' />
        public virtual void WhitespaceChars(string characters)
        {
            this.SendMessageDirect(2443, VOID.NULL, characters);
        }
        // NOTE: originally a property:Property
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="Property"]/*' />
        public virtual void Property(string key, string value)
        {
            this.SendMessageDirect(4004, key, value);
        }
        // NOTE: originally a property:KeyWords
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="KeyWords"]/*' />
        public virtual void KeyWords(int keywordSet, string keyWords)
        {
            this.SendMessageDirect(4005, keywordSet, keyWords);
        }
        // NOTE: originally a property:LexerLanguage
        /// <include file='..\Help\GeneratedInclude.xml' path='root/Methods/Method[@name="LexerLanguage"]/*' />
        public virtual void LexerLanguage(string language)
        {
            this.SendMessageDirect(4006, VOID.NULL, language);
        }
    }
}
