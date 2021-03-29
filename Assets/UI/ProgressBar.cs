using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI {
	public class ProgressBar : MinMaxSlider {
		public Color barFillColor = Color.red;
		public Color trackerFillColor = Color.white;
		public Vector2 trackerDimensions = new Vector2(0, 9);
        /// <summary>
        /// Instantiates a <see cref="MinMaxSlider"/> using the data read from a UXML file.
        /// </summary>
        public new class UxmlFactory : UxmlFactory<MinMaxSlider, UxmlTraits> {}

        /// <summary>
        /// Defines <see cref="UxmlTraits"/> for the <see cref="MinMaxSlider"/>.
        /// </summary>
        public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
        {
            UxmlFloatAttributeDescription m_MinValue = new UxmlFloatAttributeDescription { name = "min-value", defaultValue = 0 };
            UxmlFloatAttributeDescription m_MaxValue = new UxmlFloatAttributeDescription { name = "max-value", defaultValue = kDefaultHighValue };
            UxmlFloatAttributeDescription m_LowLimit = new UxmlFloatAttributeDescription { name = "low-limit", defaultValue = float.MinValue };
            UxmlFloatAttributeDescription m_HighLimit = new UxmlFloatAttributeDescription { name = "high-limit", defaultValue = float.MaxValue };

            /// <summary>
            /// Initialize <see cref="MinMaxSlider"/> properties using values from the attribute bag.
            /// </summary>
            /// <param name="ve">The element to initialize.</param>
            /// <param name="bag">The bag of attributes.</param>
            /// <param name="cc">Creation Context, unused.</param>
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var slider = ((MinMaxSlider)ve);
                slider.minValue = m_MinValue.GetValueFromBag(bag, cc);
                slider.maxValue = m_MaxValue.GetValueFromBag(bag, cc);
                slider.lowLimit = m_LowLimit.GetValueFromBag(bag, cc);
                slider.highLimit = m_HighLimit.GetValueFromBag(bag, cc);
            }
        }

        internal VisualElement dragElement { get; private set; }

        // Minimum value of the current position of the slider
        /// <summary>
        /// This is the low value of the range represented on the slider.
        /// </summary>
        public float minValue
        {
            get { return value.x; }
            set
            {
                base.value = ClampValues(new Vector2(value, rawValue.y));
            }
        }

        // Maximum value of the current position of the slider
        /// <summary>
        /// This is the high value of the range represented on the slider.
        /// </summary>
        public float maxValue
        {
            get { return value.y; }
            set
            {
                base.value = ClampValues(new Vector2(rawValue.x, value));
            }
        }

        // Complete value of the slider position, where X is the minimum, and Y is the maximum in a Vector2
        /// <summary>
        /// This is the value of the slider. This is a <see cref="Vector2"/> where the x is the lower bound and the y is the higher bound.
        /// </summary>
        public override Vector2 value
        {
            get { return base.value; }
            set
            {
                base.value = ClampValues(value);
            }
        }

        public override void SetValueWithoutNotify(Vector2 newValue)
        {
            base.SetValueWithoutNotify(ClampValues(newValue));
            UpdateDragElementPosition();
        }

        // The complete range that the value could span on, from the minimum to the maximum limit.
        /// <summary>
        /// Returns the range of the low/high limits of the slider.
        /// </summary>
        public float range
        {
            get { return Math.Abs(highLimit - lowLimit); }
        }

        float m_MinLimit;
        float m_MaxLimit;

        // This is the low limit that the slider can slide to.
        /// <summary>
        /// This is the low limit of the slider.
        /// </summary>
        public float lowLimit
        {
            get { return m_MinLimit; }
            set
            {
                if (!Mathf.Approximately(m_MinLimit, value))
                {
                    if (value > m_MaxLimit)
                    {
                        throw new ArgumentException("lowLimit is greater than highLimit");
                    }

                    m_MinLimit = value;
                    this.value = rawValue;
                    UpdateDragElementPosition();

                    //if (!string.IsNullOrEmpty(viewDataKey))
                    //    SaveViewData();
                }
            }
        }

        // This is the high limit that the slider can slide to.
        /// <summary>
        /// This is the high limit of the slider.
        /// </summary>
        public float highLimit
        {
            get { return m_MaxLimit; }
            set
            {
                if (!Mathf.Approximately(m_MaxLimit, value))
                {
                    if (value < m_MinLimit)
                    {
                        throw new ArgumentException("highLimit is smaller than lowLimit");
                    }

                    m_MaxLimit = value;
                    this.value = rawValue;
                    UpdateDragElementPosition();

                    //if (!string.IsNullOrEmpty(viewDataKey))
                    //    SaveViewData();
                }
            }
        }

        internal const float kDefaultHighValue = 10;

        /// <summary>
        /// USS class name of elements of this type.
        /// </summary>
        public new static readonly string ussClassName = "unity-min-max-slider";
        /// <summary>
        /// USS class name of labels in elements of this type.
        /// </summary>
        public new static readonly string labelUssClassName = ussClassName + "__label";
        /// <summary>
        /// USS class name of input elements in elements of this type.
        /// </summary>
        public new static readonly string inputUssClassName = ussClassName + "__input";

        /// <summary>
        /// USS class name of tracker elements in elements of this type.
        /// </summary>
        public static readonly string trackerUssClassName = ussClassName + "__tracker";
        /// <summary>
        /// USS class name of dragger elements in elements of this type.
        /// </summary>
        public static readonly string draggerUssClassName = ussClassName + "__dragger";
        /// <summary>
        /// USS class name of the minimum thumb elements in elements of this type.
        /// </summary>
        public static readonly string minThumbUssClassName = ussClassName + "__min-thumb";
        /// <summary>
        /// USS class name of the maximum thumb elements in elements of this type.
        /// </summary>
        public static readonly string maxThumbUssClassName = ussClassName + "__max-thumb";

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProgressBar()
            : this(null) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minValue">The minimum value in the range to be represented.</param>
        /// <param name="maxValue">The maximum value in the range to be represented.</param>
        /// <param name="minLimit">The minimum value of the slider limit.</param>
        /// <param name="maxLimit">The maximum value of the slider limit.</param>
        public ProgressBar(float maxValue, float minLimit, float maxLimit)
            : this(null, maxValue, minLimit, maxLimit) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minValue">The minimum value in the range to be represented.</param>
        /// <param name="maxValue">The maximum value in the range to be represented.</param>
        /// <param name="minLimit">The minimum value of the slider limit.</param>
        /// <param name="maxLimit">The maximum value of the slider limit.</param>
        public ProgressBar(string label, float maxValue = kDefaultHighValue, float minLimit = float.MinValue, float maxLimit = float.MaxValue)
            : base(label)
        {
            lowLimit = minLimit;
            highLimit = maxLimit;
            this.minValue = 0;
            this.maxValue = maxValue;
            AddToClassList(ussClassName);
            labelElement.AddToClassList(labelUssClassName);

            var trackElement = new VisualElement() { name = "unity-tracker" };
            trackElement.AddToClassList(trackerUssClassName);

            dragElement = new VisualElement() { name = "unity-dragger" };
            dragElement.AddToClassList(draggerUssClassName);
            dragElement.RegisterCallback<GeometryChangedEvent>(UpdateDragElementPosition);
            
            m_MinLimit = minLimit;
            m_MaxLimit = maxLimit;
            rawValue = ClampValues(new Vector2(0, maxValue));
            UpdateDragElementPosition();
        }

        // Clamp the actual parameter inside the low / high limit
        Vector2 ClampValues(Vector2 valueToClamp)
        {
            // Make sure the limits are ok...
            if (m_MinLimit > m_MaxLimit)
            {
                m_MinLimit = m_MaxLimit;
            }

            Vector2 clampedValue = new Vector2();

            // Make sure the value max is not bigger than the max limit...
            if (valueToClamp.y > m_MaxLimit)
            {
                valueToClamp.y = m_MaxLimit;
            }

            // Clamp both values
            clampedValue.x = Mathf.Clamp(valueToClamp.x, m_MinLimit, valueToClamp.y);
            clampedValue.y = Mathf.Clamp(valueToClamp.y, valueToClamp.x, m_MaxLimit);
            return clampedValue;
        }

        void UpdateDragElementPosition(GeometryChangedEvent evt)
        {
            // Only affected by dimension changes
            if (evt.oldRect.size == evt.newRect.size)
            {
                return;
            }

            UpdateDragElementPosition();
        }

        void UpdateDragElementPosition()
        {
            // UpdateDragElementPosition() might be called at times where we have no panel
            // we must skip the position calculation and wait for a layout pass
            if (panel == null)
                return;
            // This is the main calculation for the location of the thumbs / dragging element
            float offsetForThumbFullWidth = -dragElement.resolvedStyle.marginLeft - dragElement.resolvedStyle.marginRight;
            var sliceSpan = dragElement.resolvedStyle.unitySliceLeft + dragElement.resolvedStyle.unitySliceRight;
            var newPositionLeft = Mathf.Round(SliderLerpUnclamped(dragElement.resolvedStyle.unitySliceLeft, (layout.width + offsetForThumbFullWidth) - dragElement.resolvedStyle.unitySliceRight, SliderNormalizeValue(minValue, lowLimit, highLimit)) - dragElement.resolvedStyle.unitySliceLeft);
            var newPositionRight = Mathf.Round(SliderLerpUnclamped(dragElement.resolvedStyle.unitySliceLeft, (layout.width + offsetForThumbFullWidth) - dragElement.resolvedStyle.unitySliceRight, SliderNormalizeValue(maxValue, lowLimit, highLimit)) + dragElement.resolvedStyle.unitySliceRight);
            dragElement.style.width = Mathf.Max(sliceSpan, newPositionRight - newPositionLeft);
            dragElement.style.left = newPositionLeft;
        }

        internal float SliderLerpUnclamped(float a, float b, float interpolant)
        {
            return Mathf.LerpUnclamped(a, b, interpolant);
        }

        internal float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
        {
            return (currentValue - lowerValue) / (higherValue - lowerValue);
        }

        float ComputeValueFromPosition(float positionToConvert)
        {
            var interpolant = 0.0f;
            //interpolant = SliderNormalizeValue(positionToConvert, dragElement.resolvedStyle.unitySliceLeft, (visualInput.layout.width - dragElement.resolvedStyle.unitySliceRight));
            return SliderLerpUnclamped(lowLimit, highLimit, interpolant);
        }

        protected override void ExecuteDefaultAction(EventBase evt)
        {
            base.ExecuteDefaultAction(evt);

            if (evt == null)
            {
                return;
            }

            if (evt.eventTypeId == GeometryChangedEvent.TypeId())
            {
                UpdateDragElementPosition((GeometryChangedEvent)evt);
            }
        }

        void ComputeValueDragStateNoThumb(float lowLimitPosition, float highLimitPosition, float dragElementPos)
        {
            float newPosition;

            // Clamp the dragElementPos
            if (dragElementPos < lowLimitPosition)
            {
                newPosition = lowLimit;
            }
            else if (dragElementPos > highLimitPosition)
            {
                newPosition = highLimit;
            }
            else
            {
                newPosition = ComputeValueFromPosition(dragElementPos);
            }

            // The new position is the MAX value... here, we must keep the distance between min and max the same as it was, since this is just a drag...
            var actualDifference = (maxValue - minValue);
            var newMinValue = (newPosition - actualDifference);
            var newMaxValue = newPosition;

            if (newMinValue < lowLimit)
            {
                newMinValue = lowLimit;
                newMaxValue = newMinValue + actualDifference;
            }

            value = new Vector2(newMinValue, newMaxValue);
        }

        protected override void UpdateMixedValueContent()
        {
        }
	}
}