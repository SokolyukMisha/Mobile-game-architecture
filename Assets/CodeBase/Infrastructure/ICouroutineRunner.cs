using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface ICouroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}