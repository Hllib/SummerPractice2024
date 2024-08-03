using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BG_Games.Scripts.Base.Enums;
using BG_Games.Scripts.Base.WindowPosition;

namespace BG_Games.Scripts.Base.AnimationPresets
{
    public class AnimationPresetContainer
    {
        public readonly List<ShowAnimationPreset> ShowPresets;
        public readonly List<HideAnimationPreset> HidePresets;

        public Func<Task> this[ShowAnimationType animationType] =>
            ShowPresets.FirstOrDefault(element => element.ShowAnimationType == animationType)?.ShowTask;

        public Func<Task> this[HideAnimationType animationType] =>
            HidePresets.FirstOrDefault(element => element.HideAnimationType == animationType)?.HideAction;
        
        public AnimationPresetContainer(UIWindowAnimator animator)
        {
            WindowPositionContainer windowPositionContainer = new WindowPositionContainer();
             
            async Task AppearAbove() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Above], windowPositionContainer[PositionFromScreen.Center]);
            async Task AppearUnder() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Under], windowPositionContainer[PositionFromScreen.Center]);
            async Task AppearRight() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Right], windowPositionContainer[PositionFromScreen.Center]);
            async Task AppearLeft() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Left], windowPositionContainer[PositionFromScreen.Center]);
            async Task FadeIn() => await animator.FadeInAsync(animator._cancellationTokenSource.Token);
            async Task ScaleUp() => await animator.ScaleUpAsync(animator._cancellationTokenSource.Token);

            ShowPresets = new List<ShowAnimationPreset>()
            {
                new ShowAnimationPreset(ShowAnimationType.None, null),
                new ShowAnimationPreset(ShowAnimationType.AppearAbove, AppearAbove),
                new ShowAnimationPreset(ShowAnimationType.AppearUnder, AppearUnder),
                new ShowAnimationPreset(ShowAnimationType.AppearLeft, AppearLeft),
                new ShowAnimationPreset(ShowAnimationType.AppearRight, AppearRight),
                new ShowAnimationPreset(ShowAnimationType.ScaleUp, ScaleUp),
                new ShowAnimationPreset(ShowAnimationType.FadeIn, FadeIn),
            };

            async Task GoAbove() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Center], windowPositionContainer[PositionFromScreen.Above]);
            async Task GoUnder() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Center], windowPositionContainer[PositionFromScreen.Under]);
            async Task GoLeft() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Center], windowPositionContainer[PositionFromScreen.Left]);
            async Task GoRight() => await animator.MoveAsync(animator._cancellationTokenSource.Token, windowPositionContainer[PositionFromScreen.Center], windowPositionContainer[PositionFromScreen.Right]);
            async Task ScaleDown() => await animator.ScaleDownAsync(animator._cancellationTokenSource.Token);
            async Task FadeOut() => await animator.FadeOutAsync(animator._cancellationTokenSource.Token);

            HidePresets = new List<HideAnimationPreset>()
            {
                new HideAnimationPreset(HideAnimationType.None, null),
                new HideAnimationPreset(HideAnimationType.GoAbove, GoAbove),
                new HideAnimationPreset(HideAnimationType.GoUnder, GoUnder),
                new HideAnimationPreset(HideAnimationType.GoLeft, GoLeft),
                new HideAnimationPreset(HideAnimationType.GoRight, GoRight),
                new HideAnimationPreset(HideAnimationType.ScaleDown ,ScaleDown),
                new HideAnimationPreset(HideAnimationType.FadeOut, FadeOut),
            };
        }
    }
}