using System;
using UnityEngine;
using Photon.Deterministic;
using Quantum;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(EntityPrototype))]
public partial class EntityComponentPhysicsJoints2D {
  private void OnValidate() => AutoConfigureDistance();

  public override void Refresh() => AutoConfigureDistance();

  private void AutoConfigureDistance() {
    if (Prototype.JointConfigs == null) {
      return;
    }

    FPMathUtils.LoadLookupTables();

    foreach (var config in Prototype.JointConfigs) {
      if (config.AutoConfigureDistance && config.JointType != Quantum.Physics2D.JointType.None) {
        var anchorPos    = transform.position.ToFPVector2() + FPVector2.Rotate(config.Anchor, transform.rotation.ToFPRotation2D());
        var connectedPos = config.ConnectedAnchor;

        if (config.ConnectedEntity != null) {
          var connectedTransform = config.ConnectedEntity.transform;
          connectedPos =  FPVector2.Rotate(connectedPos, connectedTransform.rotation.ToFPRotation2D());
          connectedPos += connectedTransform.position.ToFPVector2();
        }

        config.Distance    = FPVector2.Distance(anchorPos, connectedPos);
        config.MinDistance = config.Distance;
        config.MaxDistance = config.Distance;
      }

      if (config.MinDistance > config.MaxDistance) {
        config.MinDistance = config.MaxDistance;
      }
    }
  }

#if UNITY_EDITOR
  private void OnDrawGizmos() {
    DrawGizmos(selected: false);
  }

  private void OnDrawGizmosSelected() {
    DrawGizmos(selected: true);
  }

  private void DrawGizmos(bool selected) {
    if (!QuantumGameGizmos.ShouldDraw(QuantumEditorSettings.Instance.DrawJointGizmos, selected)) {
      return;
    }

    var entity = GetComponent<EntityPrototype>();

    if (entity == null || Prototype.JointConfigs == null) {
      return;
    }

    FPMathUtils.LoadLookupTables();

    var editorSettings = QuantumEditorSettings.Instance;
    foreach (var config in Prototype.JointConfigs) {
      GizmoUtils.DrawGizmosJoint2D(config, transform, config.ConnectedEntity == null ? null : config.ConnectedEntity.transform, selected, editorSettings, editorSettings.JointGizmosStyle);
    }
  }
#endif
}