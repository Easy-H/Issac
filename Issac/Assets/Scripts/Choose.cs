using UnityEngine;

namespace Util {
    public class Choose {

        int _count = 0;
        int[] card;

        public Choose(int count)
        {
            _count = count;
            card = new int[count];

            for (int i = 0; i < count; i++) card[i] = i;
        }

        public bool TryPick(out int result)
        {

            if (IsEmpty())
            {
                result = 0;
                return false;
            }

            result = Pick();
            return true;

        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public int Pick()
        {
            int rand = Random.Range(0, _count);

            int retval = card[rand];
            card[rand] = card[_count - 1];
            card[--_count] = retval;

            return retval;

        }

    }
}