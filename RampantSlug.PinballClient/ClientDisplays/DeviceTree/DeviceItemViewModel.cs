﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class DeviceItemViewModel : Screen
    {
        readonly ObservableCollection<DeviceItemViewModel> _children;
        readonly DeviceItemViewModel _parent;
        //private IDevice _device;

        bool _isExpanded;
        bool _isSelected;


        #region Constructors

        //  public DeviceItemViewModel(IDevice device)
        //      : this(device, null)
        //  {
        //  }

        public DeviceItemViewModel(DeviceItemViewModel parent)
        {
            // _device = device;
            _parent = parent;

            _children = new ObservableCollection<DeviceItemViewModel>();


            /*     _children = new ReadOnlyCollection<DeviceItemViewModel>(
                         (from child in _device.Children
                          select new DeviceItemViewModel(child, this))
                          .ToList<DeviceItemViewModel>());          
             */
        }

        #endregion


        public ObservableCollection<DeviceItemViewModel> Children
        {
            get { return _children; }
        }

        #region Parent

        public new DeviceItemViewModel Parent
        {
            get { return _parent; }
        }

        #endregion // Parent


        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }
    }
}
