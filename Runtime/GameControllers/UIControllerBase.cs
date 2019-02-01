namespace Craiel.UnityEssentialsUI.Runtime.GameControllers
{
    using System.Collections.Generic;
    using Enums;
    using Events;
    using Runtime;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEssentials.Runtime.Contracts;
    using UnityEssentials.Runtime.Event;

    public class UIControllerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private readonly IList<BaseEventSubscriptionTicket> managedEventSubscriptions;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        protected UIControllerBase()
        {
            this.managedEventSubscriptions = new List<BaseEventSubscriptionTicket>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SerializeField] 
        public GameObject Root;

        [SerializeField] 
        public GameObject NotifyRoot;

        [SerializeField] 
        public GameObject PointerMarkerRoot;

        [SerializeField] 
        public UIControlAwakeState AwakeState;

        public bool IsHidden
        {
            get { return this.Root != null && !this.Root.activeSelf; }
        }

        public virtual void Awake()
        {
            if (this.Root == null)
            {
                EssentialCoreUI.Logger.Warn("UI Controller has no root set: {0} ({1})", this.name, this.GetType().Name);
            }

            switch (this.AwakeState)
            {
                case UIControlAwakeState.Hidden:
                {
                    this.Hide();
                    break;
                }

                case UIControlAwakeState.Visible:
                {
                    this.Show();
                    break;
                }
            }
            
            this.SubscribeEvent<EventEngineInitialized>(this.OnEngineInitialized);
        }

        public virtual void OnDestroy()
        {
            foreach (BaseEventSubscriptionTicket ticket in this.managedEventSubscriptions)
            {
                BaseEventSubscriptionTicket closure = ticket;
                GameEvents.Unsubscribe(ref closure);
            }

            this.managedEventSubscriptions.Clear();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (this.PointerMarkerRoot != null)
            {
                this.PointerMarkerRoot.SetActive(true);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (this.PointerMarkerRoot != null)
            {
                this.PointerMarkerRoot.SetActive(false);
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (this.NotifyRoot != null)
            {
                this.NotifyRoot.SetActive(false);
            }
        }
        
        public virtual void Hide()
        {
            this.ToggleRoot(false);
        }

        public virtual void Show()
        {
            this.ToggleRoot(true);
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected void SubscribeEvent<T>(BaseEventAggregate<IGameEvent>.GameEventAction<T> callback)
            where T : IGameEvent
        {
            BaseEventSubscriptionTicket ticket;
            GameEvents.Subscribe(callback, out ticket);
            this.managedEventSubscriptions.Add(ticket);
        }

        protected virtual void Notify()
        {
            if (this.NotifyRoot != null)
            {
                this.NotifyRoot.SetActive(true);
            }
        }

        protected virtual void Initialize()
        {
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void OnEngineInitialized(EventEngineInitialized eventData)
        {
            this.Initialize();
        }
        
        private void ToggleRoot(bool isVisible)
        {
            if (this.Root == null)
            {
                return;
            }

            if (isVisible && this.Root.activeSelf)
            {
                return;
            }

            if (!isVisible && !this.Root.activeSelf)
            {
                return;
            }

            this.Root.SetActive(isVisible);

#if UNITY_EDITOR
            EssentialCoreUI.Logger.Info("UICtrlToggle: {0} -> {1}", this.Root.name, isVisible);
#endif
        }
    }
}