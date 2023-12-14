namespace CharacterService.Messaging {
    public interface IRabbitMqProducer {
        public void SendCreationMessage < T > (T message);
    }
}