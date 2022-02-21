using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Behaviour2D : MonoBehaviour
{
	public const float GRAVITY = 20f;
	private const float COLLIDER_OFFSET = .03f;

	[SerializeField]
	private LayerMask collisionMask;
	[SerializeField]
	private bool applyGravity;

	private new BoxCollider2D collider;
	private readonly int rays = 4;
	private Bounds bounds;
	private bool grounded;

	private void Start()
	{
		collider = GetComponent<BoxCollider2D>();
	}

	public void Translate(Vector2 velocity)
	{
		bounds = collider.bounds;
		bounds.Expand(COLLIDER_OFFSET * -2f);

		HorizontalCollsionns(ref velocity);
		VerticalCollisions(ref velocity);

		transform.Translate(velocity);
	}

	public void ApplyGravity(ref Vector2 velocity)
	{
        if (!applyGravity)
        {
			return;
        }

		velocity.y -= GRAVITY * Time.deltaTime;
	}

	private void VerticalCollisions(ref Vector2 velocity)
	{
		float direction = velocity.normalized.y;
		float length = Mathf.Abs(velocity.y) + COLLIDER_OFFSET;

		for (int i = 0; i < rays; i++)
		{
			Vector2 ray = new Vector2(bounds.min.x, direction > 0 ? bounds.max.y : bounds.min.y);
			ray += Vector2.right * (bounds.size.x / (rays - 1) * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.up * direction, length, collisionMask);

			Debug.DrawRay(ray, Vector2.up * direction * length, Color.red);

			if (!hit)
			{
				continue;
			}

			velocity.y = (hit.distance - COLLIDER_OFFSET) * direction;
			length = hit.distance;
		}
	}

	private void HorizontalCollsionns(ref Vector2 velocity)
	{
		float direction = velocity.normalized.x;
		float length = Mathf.Abs(velocity.x) + COLLIDER_OFFSET;

		for (int i = 0; i < rays; i++)
		{
			Vector2 ray = new Vector2(direction > 0 ? bounds.max.x : bounds.min.x, bounds.min.y);
			ray += Vector2.up * (bounds.size.y / (rays - 1) * i);
			RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.right * direction, length, collisionMask);

			Debug.DrawRay(ray, Vector2.right * direction, Color.red);

			if (!hit)
			{
				continue;
			}

			velocity.x = (hit.distance - COLLIDER_OFFSET) * direction;
			length = hit.distance;
		}
	}
}
