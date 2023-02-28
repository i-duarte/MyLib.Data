using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace MyLib.Web.Common
{
    public class ViewState<T>
    {
        private readonly string _nombre;
        private readonly StateBag _viewState;

        public ViewState(
            string nombre
            , StateBag viewState
        )
        {
            _viewState = viewState;
            _nombre = nombre;
        }

        public ViewState(
            string nombre
            , StateBag viewState
            , T valorDefault
        )
        {
            _viewState = viewState;
            _nombre = nombre;

            if (_viewState[_nombre] == null)
            {
                Valor = valorDefault;
            }
        }

        public T Valor
        {
            get
            {
                if (_viewState[_nombre] == null)
                {
                    return default;
                }
                else
                {
                    return (T)_viewState[_nombre];
                }
            }

            set
            {
                _viewState[_nombre] = value;
            }
        }
    }
}
