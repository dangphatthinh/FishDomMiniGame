﻿using UnityEngine;
using System.Collections;

namespace Athena.Common
{
    public interface TransitionDelegate
    {
        IEnumerator Execute();
    }

    public class SceneTransitionSequential : TransitionDelegate
    {
        public SceneLoader SceneLoader = null;

        public System.Action onStepOutEnter;
        public System.Action onStepOutDidFinish;
        public System.Action onStepInEnter;
        public System.Action onStepInDidFinish;

        public IEnumerator Execute()
        {
            Enter();

            if (SceneLoader != null)
            {
                SceneLoader.PreLoad();
            }

            onStepOutEnter?.Invoke();

            while (!StepOut(Time.deltaTime))
                yield return null;

            onStepOutDidFinish?.Invoke();

            if (SceneLoader != null)
            {
                SceneLoader.PreLoadDone();
                SceneLoader.Load();
            }

            if (SceneLoader != null)
            {
                while (!SceneLoader.IsFinished())
                {
                    StepLoad(Time.deltaTime);
                    yield return null;
                }

                SceneLoader.ActiveScene();
            }

            onStepInEnter?.Invoke();

            while (!StepIn(Time.deltaTime))
                yield return null;

            onStepInDidFinish?.Invoke();

            End();
        }

        public virtual void StepLoad(float dt)
        {

        }

        public virtual bool StepOut(float dt)
        {
            return true;
        }

        public virtual bool StepIn(float dt)
        {
            return true;
        }

        public virtual void Enter()
        {

        }

        public virtual void End()
        {

        }
    }

    public class SceneTransitionConcurrent : TransitionDelegate
    {
        public SceneLoader sceneLoader;

        public IEnumerator Execute()
        {
            Enter();

            if (sceneLoader != null)
            {
                sceneLoader.Load();

                yield return null;

                sceneLoader.ActiveScene();

                while (!sceneLoader.IsFinished())
                {
                    StepLoad(Time.deltaTime);
                    yield return null;
                }
            }

            while (!Step(Time.deltaTime))
                yield return null;

            End();
        }

        public virtual void StepLoad(float dt)
        {

        }

        public virtual bool Step(float dt)
        {
            return true;
        }

        public virtual void Enter()
        {

        }

        public virtual void End()
        {

        }
    }
}