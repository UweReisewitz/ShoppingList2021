using System;
using Xamarin.Forms;

namespace ShoppingList2021.Behaviors
{
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T bindable)
        {
            if (bindable == null)
            {
                throw new ArgumentNullException(nameof(bindable));
            }

            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += BindableBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            if (bindable == null)
            {
                throw new ArgumentNullException(nameof(bindable));
            }

            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= BindableBindingContextChanged;
            AssociatedObject = null;
        }

        private void BindableBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}

