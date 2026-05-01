using UnityEngine;
using UnityEngine.InputSystem;

namespace PrototypeGames
{
    public interface IPlayerAction
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
