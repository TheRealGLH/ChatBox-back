namespace CharacterService.Messaging {
    public interface IRabitMQProducer {
        public void SendCreationMessage < T > (T message);
    }
}