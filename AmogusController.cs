using GameNetcodeStuff;
using UnityEngine;

namespace AmogusModels
{
    public class AmogusController : MonoBehaviour
    {
        GameObject amogusObj = null;
        void Start()
        {
            gameObject.GetComponentInChildren<LODGroup>().enabled = false;
            var meshes = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var LODmesh in meshes)
            {
                LODmesh.enabled = false;
            }
            //uhhhh lazy fix for improperly set up materials in my assetbundle :/ dont @ me
            //could have tried harder to fix
            GameObject amogusNoHands = (GameObject)LC_API.BundleAPI.BundleLoader.assets["ASSETS/ORIGINAL/ASTRONAUT VRC 3.0.FBX"];
            GameObject amogus = (GameObject)LC_API.BundleAPI.BundleLoader.assets["ASSETS/AMOGUS.FBX"];
            RuntimeAnimatorController animController = (RuntimeAnimatorController)LC_API.BundleAPI.BundleLoader.assets["ASSETS/CREWMATECONTROLLER.CONTROLLER"];
            Material outline = (Material)LC_API.BundleAPI.BundleLoader.assets["ASSETS/ORIGINAL/MATERIALS/OUTLINE.MAT"];
            var newAmogus = Instantiate(amogus);
            newAmogus.transform.localScale = new Vector3(5, 5, 5);
            var rig = gameObject.transform.Find("ScavengerModel").Find("metarig");
            var spine = rig.Find("spine").Find("spine.001");
            newAmogus.transform.SetParent(spine);
            newAmogus.transform.localPosition = new Vector3(0, -1.3f, 0);
            newAmogus.transform.localEulerAngles = Vector3.zero;

            var LOD1 = gameObject.GetComponent<PlayerControllerB>().thisPlayerModel;
            var goodShader = LOD1.material.shader;

            var mesh = newAmogus.GetComponentInChildren<SkinnedMeshRenderer>();
            var noHandsMesh = amogusNoHands.GetComponentInChildren<SkinnedMeshRenderer>();

            mesh.materials[0].shader = noHandsMesh.materials[0].shader;
            mesh.materials[0].color = new Color(0, 0, 0);
            mesh.materials[1].shader = goodShader;
            mesh.materials[1].color = new Color(0.6f, 0.6f, 1f);
            mesh.materials[2].shader = goodShader;
            mesh.materials[2].color = new Color(1f, 0f, 0f);

            var anim = newAmogus.GetComponentInChildren<Animator>();
            anim.runtimeAnimatorController = animController;
            var ikController = newAmogus.AddComponent<IKController>();
            var lthigh = rig.Find("spine").Find("thigh.L");
            var rthigh = rig.Find("spine").Find("thigh.R");
            var lshin = lthigh.Find("shin.L");
            var rshin = rthigh.Find("shin.R");
            var lfoot = lshin.Find("foot.L");
            var rfoot = rshin.Find("foot.R");

            var chest = rig.Find("spine").Find("spine.001").Find("spine.002").Find("spine.003");
            var lshoulder = chest.Find("shoulder.L");
            var rshoulder = chest.Find("shoulder.R");
            var lUpperArm = lshoulder.Find("arm.L_upper");
            var rUpperArm = rshoulder.Find("arm.R_upper");
            var lLowerArm = lUpperArm.Find("arm.L_lower");
            var rLowerArm = rUpperArm.Find("arm.R_lower");
            var lHand = lLowerArm.Find("hand.L");
            var rHand = rLowerArm.Find("hand.R");

            GameObject lFootOffset = new("IK Offset");
            lFootOffset.transform.SetParent(lfoot, false);
            lFootOffset.transform.localPosition = new Vector3(0f, -0.2327f, 0f);
            GameObject rFootOffset = new("IK Offset");
            rFootOffset.transform.SetParent(rfoot, false);
            rFootOffset.transform.localPosition = new Vector3(0f, 0f, 0.2549f);
            GameObject lHandOffset = new("IK Offset");
            lHandOffset.transform.SetParent(lHand, false);
            lHandOffset.transform.localPosition = new Vector3(0.2093f, 0.0783f, 0.3309f);
            GameObject rHandOffset = new("IK Offset");
            rHandOffset.transform.SetParent(rHand, false);
            rHandOffset.transform.localPosition = new Vector3(-0.2343f, 0.0456f, 0.2964f);

            ikController.leftLegTarget = lFootOffset.transform;
            ikController.rightLegTarget = rFootOffset.transform;
            ikController.leftHandTarget = lHandOffset.transform;
            ikController.rightHandTarget = rHandOffset.transform;
            ikController.ikActive = true;

            amogusObj = newAmogus;
        }

        private void LateUpdate()
        {
            if (amogusObj != null)
            {
                amogusObj.transform.localPosition = new Vector3(0, -0.45f, 0);
                var rig = gameObject.transform.Find("ScavengerModel").Find("metarig");
                var trans = rig.Find("spine").Find("spine.001").Find("spine.002").Find("spine.003");
                amogusObj.transform.Find("Armature").Find("Hips").Find("Spine").Find("Chest").localEulerAngles = trans.localEulerAngles;
            }
        }
    }
}
