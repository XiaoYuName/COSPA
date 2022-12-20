using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace ARPG
{
    public class Character : MonoBehaviour
    {
        private CharacterState State;
        private TextMeshPro NameTextUI;
        private CharacterConfigInfo data;
        private SkeletonMecanim Spine;
        private Animator anim;

        private void Awake()
        {
            NameTextUI = transform.Find("CharacterName").GetComponent<TextMeshPro>();
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
            anim = Spine.GetComponent<Animator>();
        }

        public void Init(CharacterBag bag)
        {
            data = InventoryManager.Instance.GetCharacter(bag.ID);
            State = bag.CurrentCharacterState;
            NameTextUI.text = data.CharacterName;
            Spine.skeletonDataAsset = data.SpineAsset;
            Spine.Initialize(true);
            anim.runtimeAnimatorController = data.AnimatorController;
        }
    }
}

