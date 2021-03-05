using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAvatarManager : MonoBehaviour
{
    private OvrAvatar avatar = null;

    private void Start()
    {
        avatar = GetComponent<OvrAvatar>();
    }

    public void ShowOrHideRightHand()
    {
        if (avatar.HandRight.enabled)
        {
            avatar.HandRight.enabled = false;
            avatar.HandRight.RenderParts[0].mesh.enabled = false;
        }
        else
        {
            avatar.HandRight.enabled = true;
            // No need to enable the SkinnedMeshRenderer, enabling the hand will do it for you
        }
    }
    public void ShowOrHideLeftHand()
    {
        if (avatar.HandLeft.enabled)
        {
            avatar.HandLeft.enabled = false;
            avatar.HandLeft.RenderParts[0].mesh.enabled = false;
        }
        else
        {
            avatar.HandLeft.enabled = true;
            // No need to enable the SkinnedMeshRenderer, enabling the hand will do it for you
        }
    }
}
