# Notes

## Assumptions

- Palindrome comparison uses `char.ToLowerInvariant`. Case folding does not depend on the current culture. A character that would expand into more than one character when lowercased (rare outside ASCII) is compared as a single `char`.
- A permission matches only when user, resource, and action are all equal with `==`, which is ordinal and case-sensitive. `null` arguments do not throw. They match only a stored permission that has `null` in that same field. An empty permission list allows nothing.
- Event handlers are keyed by the exact generic type passed to `Subscribe` and `Publish`. Publishing an `OrderPlaced` does not call handlers registered for a base type or another type.
- The event bus is single-threaded. `Publish` snapshots the handler list first, so a handler that subscribes during publish does not run for that same publish.
- If a handler throws, the exception propagates and later handlers for that publish do not run.
- `Subscribe(null)` throws `ArgumentNullException`.

## Trade-offs

- The palindrome check walks inward with two indexes and never builds a cleaned string. That keeps the method allocation-free and covers the optional palindrome extension. A cleaned-string version would be shorter to read and would allocate on every call.
- Permissions stay a linear scan over the array the constructor already copies. The lists in this exercise are small. A lookup keyed by user would matter only with a much larger permission set.
- Handlers are stored as `Delegate` in a dictionary of lists. That keeps one collection for every event type and preserves subscription order.

## Left for later

- Permission wildcards (`"*"` on resource and/or action), including whether a literal `"*"` is still matchable and whether the user field can be a wildcard.
- Unsubscribe, and a policy that keeps calling the remaining handlers after one throws (collect exceptions, or isolate each handler).
- Thread safety if publish and subscribe can overlap.

## Time

About 60–90 minutes, including planning and setup, within the suggested window.

## AI use

Cursor, with Grok, was used to plan the approach and write the implementation and tests. The public method signatures are unchanged. Every line is in the three exercise files, their test files, and this note, and should be explainable in the follow-up.
