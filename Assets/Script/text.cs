// public void Chase() 
    // {
    //     navAgent.isStopped = false;
    //     navAgent.speed = runSpeed;

    //     navAgent.SetDestination(target.transform.position); // set the destination towards the player
        
    //     if(navAgent.velocity.sqrMagnitude > 0f) 
    //     {
    //         // play run animation
    //     }
    //     else 
    //     {
    //         // stop run animation
    //     }

    //     if(Vector3.Distance(transform.position, target.transform.position) <= attack_distance) 
    //     {
    //         state = EnemyState.ATTACK;

    //         // reset the chase distance back to default
    //         if(chase_distance != currentChase_distance) 
    //         {
    //             chase_distance = currentChase_distance;
    //         }
    //     } 
    //     else if (Vector3.Distance(transform.position, target.transform.position) > chase_distance)
    //     {
    //         state = EnemyState.PATROL;

    //         // reset the chase distance back to default
    //         patrol_counter = patrol_time;
    //         if(chase_distance != currentChase_distance) 
    //         {
    //             chase_distance = currentChase_distance;
    //         }
    //     }
    // } // chase

    // public void Attack() 
    // {
    //     navAgent.velocity = Vector3.zero;
    //     navAgent.isStopped = true;

    //     attack_time += Time.deltaTime; // start the attack timer
    
    //     if(attack_time > attack_counter)
    //     {
    //         attack_time = 0f; // reset the attack timer
    //         // start attacking

    //         // play animation

    //         if(Vector3.Distance(transform.position, target.transform.position) > attack_distance) 
    //         {
    //             // stop attacking and start chasing again
    //             state = EnemyState.CHASE;

    //             // stop attack animation
    //         }

    //     }
    // } // attack

       // public void SetNewRandomDirection() 
    // {
    //     float radius = Random.Range(minPatrol_distance, maxPatrol_distance);
    //     Vector3 direction = Random.insideUnitSphere * radius;

    //     direction += transform.position;

    //     NavMeshHit navHit;
    //     NavMesh.SamplePosition(direction, out navHit, radius, -1);

    //     navAgent.SetDestination(navHit.position);
    // }
