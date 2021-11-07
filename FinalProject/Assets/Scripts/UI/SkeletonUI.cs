using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class SkeletonUI : MonoBehaviour
{

    public static void ShowHint(Hand hand, int index)
    {
        ControllerButtonHints.HideAllTextHints(hand);
        ISteamVR_Action_In action = SteamVR_Input.actionsIn[index];
        if (action.GetActive(hand.handType))
        {
            ControllerButtonHints.ShowTextHint(hand, action, action.GetShortName());
        }
    }

    public static void ShowController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.ShowController(true);
            }
        }
    }

    public static void HideController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.HideController(true);
            }
        }
    }
}
